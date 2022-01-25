
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;

using SimplexNoise;
using OpenSimplex;

namespace Qublock.ProceduralGeneration {

    public static class ProceduralGenerator {

        // This is a Messy-Test!
        // Replace the contents of this function with whatever you please.
        public static ChunkData GenerateChunkData (ChunkData chunk) {

            for (int x = 0; x < 32; ++x)
                for (int z = 0; z < 32; ++z) {

                    int wx = chunk.position.x + x;
                    int wz = chunk.position.z + z;

                    switch ((int)(BiomeGd.BiomeFleck.Get(wx, wz) * 3)) {

                        case 0: {

                            int height = Mathf.CeilToInt(
                                // (float)noise.Evaluate(wx * 0.032f, 0, wz * 0.032f) * 8 + 32
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 2 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height) chunk[x, y, z] = 1;
                            }

                        break; }

                        case 1: {

                            int height = Mathf.CeilToInt(
                                // (float)noise.Evaluate(wx * 0.032f, 0, wz * 0.032f) * 8 + 32
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.064f, 0, wz * 0.064f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 4 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height) chunk[x, y, z] = 1;
                            }

                        break; }

                        case 2: {

                            int height = Mathf.CeilToInt(
                                // (float)noise.Evaluate(wx * 0.032f, 0, wz * 0.032f) * 8 + 32
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 16 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height) chunk[x, y, z] = 1;
                            }

                        break; }
                    }
                }

            return chunk;
        }
    }
}
