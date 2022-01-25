
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.ProceduralGeneration.Structures;

using SimplexNoise;
using OpenSimplex;

namespace Qublock.ProceduralGeneration {

    public static class ProceduralGenerator {

        // This is a Messy - Test / Example!
        // Replace the contents of this function with whatever you please.
        public static ChunkData GenerateChunkData (ChunkData chunk) {

            System.Random rng = new System.Random();

            for (int x = 0; x < 32; ++x)
                for (int z = 0; z < 32; ++z) {

                    int wx = chunk.position.x + x;
                    int wz = chunk.position.z + z;

                    switch ((int)(BiomeGd.BiomeFleck.Get(wx, wz) * 3)) {

                        case 0: { // Desert..

                            int height = Mathf.CeilToInt(
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.064f, 0, wz * 0.064f) +
                                    SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 2 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height - 4) chunk[x, y, z] = 8;
                                else if (wy < height) chunk[x, y, z] = 3;
                                else if (wy == height) { chunk[x, y, z] = 3;
                                    if (rng.Next(0, 56) == 0)
                                        for (int yy = 1; yy <= rng.Next(3, 6); ++yy)
                                            StructureCache.Add(wx, wy + yy, wz, 14);
                                } else if (wy <= 28) chunk[x, y, z] = 5;
                            }

                        break; }

                        case 1: { // Fields..

                            int height = Mathf.CeilToInt(
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.064f, 0, wz * 0.064f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 4 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height - 3) chunk[x, y, z] = 8;
                                else if (wy < height) chunk[x, y, z] = (ushort)(height <= 30 ? 3 : 7);
                                else if (wy == height) { chunk[x, y, z] = (ushort)(height <= 30 ? 3 : 11);
                                    if (rng.Next(0, 86) == 0) {
                                        for (int xx = -2; xx <= 3; ++xx)
                                            for (int yy = -2; yy <= 3; ++yy)
                                                for (int zz = -2; zz <= 3; ++zz)
                                                    StructureCache.Add(wx + xx, wy + yy + 6, wz + zz, 10);
                                        for (int yy = 1; yy <= 6; ++yy)
                                            StructureCache.Add(wx, wy + yy, wz, 6);
                                    }
                                } else if (wy <= 28) chunk[x, y, z] = 5;
                            }

                        break; }

                        case 2: { // Hills..

                            int height = Mathf.CeilToInt(
                                (
                                    SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                                    SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                                ) * 16 + 32
                            );

                            for (int y = 0; y < 32; ++y) {

                                int wy = chunk.position.y + y;

                                if (wy < height) chunk[x, y, z] = 1;

                                if (wy < height) chunk[x, y, z] = 8;
                                else if (wy == height) chunk[x, y, z] = (ushort)(height <= 30 ? 3 : 11);
                                else if (wy <= 28) chunk[x, y, z] = 5;
                            }

                        break; }
                    }
                }

            // return chunk;
            return StructureCache.Check(chunk);
        }
    }
}
