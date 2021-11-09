
using UnityEngine;

using System;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;

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

            data.chunks[loc].Render();
            data.chunks[loc].Asign();
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
    }
}
