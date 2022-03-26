
using UnityEngine;

using System;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.Core.ThreadMesher;

namespace Qublock.Core {

    public static class World {

        public static WorldData data = new WorldData();

        public static Material chunkMaterial, fadeChunkMaterial, particleChunkMaterial;


        public static bool ChunkLoaded (ChunkLoc loc)
            => data.chunks.ContainsKey(loc);

        public static bool ChunkFilled (ChunkLoc loc)
            => ChunkLoaded(loc) ? data.chunks[loc].valuesSet : false;

        public static bool ChunkRendered (ChunkLoc loc)
            => ChunkLoaded(loc) ? data.chunks[loc].rendered : false;


        public static bool PositionIsLoaded (int x, int y, int z)
            => ChunkLoaded(ChunkLoc.FromWorldPos(x, y, z));

        public static bool PositionIsRendered (int x, int y, int z) {

            ChunkLoc loc = ChunkLoc.FromWorldPos(x, y, z);

            if (!ChunkLoaded(loc)) return false;

            return data.chunks[loc].rendered;
        }


        public static void CreateChunk (ChunkLoc loc) {

            data.chunks.Add(loc, new Chunk(loc, chunkMaterial, fadeChunkMaterial));
        }

        public static void DestroyChunk (ChunkLoc loc) {

            data.chunks[loc].Erase();
            data.chunks.Remove(loc);
        }

        public static void RenderChunk (ChunkLoc loc) {

            ThreadMeshing.Enqueue(data.chunks[loc]);
        }

        public static void RenderIfVisible (ChunkLoc loc) {

            if (!ChunkLoaded(loc)) return;
            if (!data.chunks[loc].rendered) return;

            RenderChunk(loc);
        }

        public static void Clear () {

            foreach (var chunk in data.chunks.Values)
                chunk.Erase();

            data.chunks.Clear();
        }

        // To be called from the local client, when editing blocks. Say by the player.
        public static void EditBlock (int x, int y, int z, ushort id) {

            if (data[x, y, z] == id) return;

            // if (id == 0) EffectManager.PlayBreakEffect
            // else EffectManager.PlayPlaceEffect

            data[x, y, z] = id;
            RenderBlock(x, y, z);

            // This should send a message to the server or something of the likes.
            // However, here we shall simply edit the "server" directly.
            Qublock.Data.Managers.ChunkManager.SetWorldValue(x, y, z, id);
        }

        // To be called by a Network-Manager of some sort when a block is edited say by another player.
        public static void ChangeBlock (int x, int y, int z, ushort id) {

            if (data[x, y, z] == id) return;

            data[x, y, z] = id;
            RenderBlock(x, y, z);
        }

        public static void RenderBlock (int x, int y, int z) {

            RenderChunk(ChunkLoc.FromWorldPos(x, y, z));

            if ((x & 0x1F) == 31) RenderChunk(ChunkLoc.FromWorldPos(x + 1, y, z));
            if ((x & 0x1F) ==  0) RenderChunk(ChunkLoc.FromWorldPos(x - 1, y, z));

            if ((y & 0x1F) == 31) RenderChunk(ChunkLoc.FromWorldPos(x, y + 1, z));
            if ((y & 0x1F) ==  0) RenderChunk(ChunkLoc.FromWorldPos(x, y - 1, z));

            if ((z & 0x1F) == 31) RenderChunk(ChunkLoc.FromWorldPos(x, y, z + 1));
            if ((z & 0x1F) ==  0) RenderChunk(ChunkLoc.FromWorldPos(x, y, z - 1));
        }
    }
}
