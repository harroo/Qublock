
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;

using SimplexNoise;
using OpenSimplex;

namespace Qublock.ProceduralGeneration {

    public static class ProceduralGenerator {

        public static ChunkData GenerateChunkData (ChunkData chunk) {

            for (int x = 0; x < 32; ++x)
                for (int z = 0; z < 32; ++z) {

                    int wx = chunk.position.x + x;
                    int wz = chunk.position.z + z;

                    int height = Mathf.CeilToInt(
                        // (float)noise.Evaluate(wx * 0.032f, 0, wz * 0.032f) * 8 + 32
                        SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) * 8 + 32
                    );

                    for (int y = 0; y < 32; ++y) {

                        int wy = chunk.position.y + y;

                        if (wy < height) chunk[x, y, z] = 1;
                    }
                }

            return chunk;
        }
    }
}
