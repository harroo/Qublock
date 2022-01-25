
using System.Collections.Generic;
using System.Threading;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;

namespace Qublock.ProceduralGeneration.Structures {

    public static class StructureCache {

        private static Mutex mutex = new Mutex();

        private static Dictionary<GridPos, ushort> generationNodes
            = new Dictionary<GridPos, ushort>();

        private static List<GridPos> clearQueue = new List<GridPos>();

        public static void Add (int x, int y, int z, ushort id) {

            mutex.WaitOne(); try {

                // generationNodes.Add(new GridPos(x, y, z), id);
                generationNodes[new GridPos(x, y, z)] = id;

            } finally { mutex.ReleaseMutex(); }
        }

        public static ChunkData Check (ChunkData chunk) {

            mutex.WaitOne(); try {

                if (generationNodes.Count == 0) return chunk;

                foreach (var node in generationNodes) {

                    if (ChunkLoc.FromWorldPos(node.Key.x, node.Key.y, node.Key.z) == chunk.location) {

                        chunk[node.Key.x & 0x1F, node.Key.y & 0x1F, node.Key.z & 0x1F] = node.Value;
                        clearQueue.Add(node.Key);
                    }
                }

                while (clearQueue.Count != 0) {

                    generationNodes.Remove(clearQueue[0]);
                    clearQueue.RemoveAt(0);
                }

                return chunk;

            } finally { mutex.ReleaseMutex(); }
        }
    }
}
