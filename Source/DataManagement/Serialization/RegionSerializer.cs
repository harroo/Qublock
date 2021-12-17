
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Qublock.Data.Storage.Containers;
using Qublock.Data.Storage.Structures;
using Qublock.Data.Serialization.Compression;

namespace Qublock.Data.Serialization {

    public static class Serializer {

        public static string savePath = "save/";

        public static void Serialize (RegionData region) {

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            List<byte> buffer = new List<byte>();

            foreach (var chunk in region.GetChunks()) {

                ushort[] values = CachedRunLengthEncoding.Encode(chunk.GetValues());
                byte[] chunkBuffer = new byte[values.Length * 2];
                Buffer.BlockCopy(values, 0, chunkBuffer, 0, chunkBuffer.Length);

                foreach (var b in BitConverter.GetBytes(chunk.location.X)) buffer.Add(b);
                foreach (var b in BitConverter.GetBytes(chunk.location.Y)) buffer.Add(b);
                foreach (var b in BitConverter.GetBytes(chunk.location.Z)) buffer.Add(b);

                foreach (var b in BitConverter.GetBytes((ushort)chunkBuffer.Length)) buffer.Add(b);

                foreach (var b in chunkBuffer) buffer.Add(b);
            }

            File.WriteAllBytes(
                savePath + region.location.X + "." + region.location.Y + "." + region.location.Z,
                GZipCompression.Compress(buffer.ToArray())
            );
        }

        public static RegionData Deserialize (RegionLoc loc) {

            byte[] raw = GZipCompression.Decompress(File.ReadAllBytes(
                savePath + loc.X + "." + loc.Y + "." + loc.Z
            ));

            RegionData region = new RegionData(loc);

            ChunkData[] chunks = new ChunkData[8 * 8 * 8];
            int bufferIndex = 0, chunkIndex = 0;

            while (bufferIndex < raw.Length) {

                chunks[chunkIndex] = new ChunkData(new ChunkLoc(
                    BitConverter.ToInt32(raw, bufferIndex),
                    BitConverter.ToInt32(raw, bufferIndex += 4),
                    BitConverter.ToInt32(raw, bufferIndex += 4)
                ));

                int length = BitConverter.ToUInt16(raw, bufferIndex += 4);
                ushort[] values = new ushort[length];

                Buffer.BlockCopy(raw, bufferIndex += 2, values, 0, length);
                values = CachedRunLengthEncoding.Decode(values);

                chunks[chunkIndex].SetValues(values);

                bufferIndex += length;
                chunkIndex++;
            }

            region.SetChunks(chunks);
            return region;
        }

        public static bool RegionFileExists (RegionLoc loc)
            => File.Exists(savePath + loc.X + "." + loc.Y + "." + loc.Z);
    }
}
