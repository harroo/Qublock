
using System;

using BiomeGd.NoiseDeps;

namespace BiomeGd {

    public static class BiomeFleck {

        private static FastNoiseLite genNoise = new FastNoiseLite();
        private static FastNoiseLite warpNoise = new FastNoiseLite();

        private static bool initialized = false;

        private static void Initialize () { initialized = true;

            genNoise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            genNoise.SetRotationType3D(FastNoiseLite.RotationType3D.None);
            genNoise.SetSeed(1337);
            genNoise.SetFrequency(0.015f);

            genNoise.SetFractalType(FastNoiseLite.FractalType.None);
            genNoise.SetFractalOctaves(4);
            genNoise.SetFractalLacunarity(2.0f);
            genNoise.SetFractalGain(0.90f);
            genNoise.SetFractalWeightedStrength(0.70f);
            genNoise.SetFractalPingPongStrength(3.0f);

            genNoise.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.Hybrid);
            genNoise.SetCellularReturnType(FastNoiseLite.CellularReturnType.CellValue);
            genNoise.SetCellularJitter(1.0f);

            warpNoise.SetSeed(1337);
            warpNoise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
            warpNoise.SetRotationType3D(FastNoiseLite.RotationType3D.None);
            warpNoise.SetDomainWarpAmp(100.0f);
            warpNoise.SetFrequency(0.010f);
            warpNoise.SetFractalType(FastNoiseLite.FractalType.DomainWarpIndependent);
            warpNoise.SetFractalOctaves(3);
            warpNoise.SetFractalLacunarity(2.0f);
            warpNoise.SetFractalGain(0.5f);
        }

        public static float Get (float x, float y) {

            return Get(x, y, 1.0f);
        }

        public static float Get (float x, float y, float s) {

            if (!initialized) Initialize();

            float xf = x, yf = y;

            for (int i = 0; i < 2; ++i) {

                warpNoise.DomainWarp(ref xf, ref yf);

                xf -= x; yf -= y;
            }

            return Math.Abs(genNoise.GetNoise(xf * s, yf * s));
        }
    }

    public static class BiomeBlot {

        private static FastNoiseLite genNoise = new FastNoiseLite();

        private static bool initialized = false;

        private static void Initialize () { initialized = true;

            genNoise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
            genNoise.SetRotationType3D(FastNoiseLite.RotationType3D.None);
            genNoise.SetFrequency(0.015f);

            genNoise.SetFractalType(FastNoiseLite.FractalType.None);
            genNoise.SetFractalOctaves(4);
            genNoise.SetFractalLacunarity(2.0f);
            genNoise.SetFractalGain(0.9f);
            genNoise.SetFractalWeightedStrength(0.7f);
            genNoise.SetFractalPingPongStrength(3.0f);

            genNoise.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.Hybrid);
            genNoise.SetCellularReturnType(FastNoiseLite.CellularReturnType.CellValue);
            genNoise.SetCellularJitter(1.0f);

            genNoise.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
            genNoise.SetDomainWarpAmp(100.0f);
        }

        public static float Get (float x, float y) {

            if (!initialized) Initialize();

            return Math.Abs(genNoise.GetNoise(x, y));
        }

        public static float Get (float x, float y, float s) {

            return Get(x * s, y * s);
        }
    }
}
