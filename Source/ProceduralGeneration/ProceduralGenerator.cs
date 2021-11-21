
using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;

namespace Qublock.ProceduralGeneration {

    public static class ProceduralGenerator {

        public static ChunkData GenerateChunkData (ChunkData chunk) {

            chunk[0, 0, 0] = 1;

            return chunk;
        }
    }
}
