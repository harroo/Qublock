
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.ProceduralGeneration.Structures;

using SimplexNoise;
using OpenSimplex;

namespace Qublock.ProceduralGeneration {

    public static class ProceduralGenerator {

        static int xMin, xMax;
        static int zMin, zMax;

        static int bottomLeft, topLeft;
        static int bottomRight, topRight;

        static System.Random rng = new System.Random();

        // This is a Messy - Test / Example!
        // Replace the contents of this function with whatever you please.
        public static ChunkData GenerateChunkData (ChunkData chunk) {

            int cell = 4, mult = 8;
            // switch (rng.Next(0, 3)) {
            //
            //     case 0: cell = 2; mult = 16; break;
            //     case 1: cell = 4; mult = 8; break;
            //     case 2: cell = 8; mult = 4; break;
            // }

            for (int i = 1; i <= cell; i++) {

                for (int ii = 1; ii <= cell; ii++) {

                    xMin = chunk.position.x + ((i - 1) * mult);
                    xMax = chunk.position.x + (i * mult);
                    zMin = chunk.position.z + ((ii - 1) * mult);
                    zMax = chunk.position.z + (ii * mult);

                    bottomLeft = GetMasterHeight(xMin, zMin);
                    topLeft = GetMasterHeight(xMin, zMax);
                    bottomRight = GetMasterHeight(xMax, zMin);
                    topRight = GetMasterHeight(xMax, zMax);

                    for (int x = xMin; x < xMax; x++) {

                        for (int z = zMin; z < zMax; z++) {

                            int height = Mathf.RoundToInt(
                                SmoothInterpolateBilinearly(
                                    bottomLeft, topLeft, bottomRight, topRight,
                                    xMin, xMax, zMin, zMax,
                                    x, z
                                )
                            );

                            FillStack(chunk, x & 0x1F, z & 0x1F, height);
                        }
                    }
                }
            }

            // return chunk;
            return StructureCache.Check(chunk);
        }

        private static int GetMasterHeight (int wx, int wz) {

            switch ((int)(BiomeGd.BiomeFleck.Get(wx, wz) * 3)) {

                case 0: { // Desert..

                    return Mathf.CeilToInt(
                        (
                            SimplexNoise.Noise.Generate(wx * 0.064f, 0, wz * 0.064f) +
                            SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                            SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                        ) * 2 + 32
                    );
                }

                case 1: { // Fields..

                    return Mathf.CeilToInt(
                        (
                            SimplexNoise.Noise.Generate(wx * 0.064f, 0, wz * 0.064f) +
                            SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                        ) * 4 + 32
                    );
                }

                case 2: { // Hills..

                    return Mathf.CeilToInt(
                        (
                            SimplexNoise.Noise.Generate(wx * 0.032f, 0, wz * 0.032f) +
                            SimplexNoise.Noise.Generate(wx * 0.016f, 0, wz * 0.016f)
                        ) * 16 + 32
                    );
                }

                default: return 0;
            }
        }

        private static void FillStack (ChunkData chunk, int x, int z, int height) {

            int wx = chunk.position.x + x;
            int wz = chunk.position.z + z;

            switch ((int)(BiomeGd.BiomeFleck.Get(wx, wz) * 3)) {

                case 0: { // Desert..

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

        // Imported..
        private static float smoothstep (float edge0, float edge1, float x) {
            // Scale, bias and saturate x to 0..1 range
            x = x * x * (3 - 2 * x);
            // Evaluate polynomial
            return (edge0 * x) + (edge1 * (1 - x));
        }
        private static float SmoothInterpolateBilinearly (float bottomLeft, float topLeft, float bottomRight, float topRight, float xMin, float xMax, float zMin, float zMax, float x, float z) {
            float width = xMax - xMin;
            float height = zMax - zMin;
            float xValue = 1 - (x - xMin) / width;
            float zValue = 1 - (z - zMin) / height;
            float a = smoothstep(bottomLeft, bottomRight, xValue);
            float b = smoothstep(topLeft, topRight, xValue);
            return smoothstep(a, b, zValue);
        }
        private static float InterpolateBilinearly (float bottomLeft, float topLeft, float bottomRight, float topRight, float xMin, float xMax, float zMin, float zMax, float x, float z) {
            float width = xMax - xMin;
            float height = zMax - zMin;
            float xDistanceToMaxValue = xMax - x;
            float zDistanceToMaxValue = zMax - z;
            float xDistanceToMinValue = x - xMin;
            float zDistanceToMinValue = z - zMin;
            return 1.0f / (width * height) * (
                bottomLeft * xDistanceToMaxValue * zDistanceToMaxValue +
                bottomRight * xDistanceToMinValue * zDistanceToMaxValue +
                topLeft * xDistanceToMaxValue * zDistanceToMinValue +
                topRight * xDistanceToMinValue * zDistanceToMinValue
            );
        }
    }
}
