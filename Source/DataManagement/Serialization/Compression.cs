
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Threading;

namespace Qublock.Data.Serialization.Compression {

    public static class RunLengthEncoding {

        public static ushort[] Encode (ushort[] data) {

            List<ushort> encodedData = new List<ushort>();
            ushort currentType = data[0], currentCount = 1;

            for (int i = 1; i < data.Length; ++i) {

                if (data[i] == currentType) {

                    currentCount++;

                } else {

                    encodedData.Add(currentType);
                    encodedData.Add(currentCount);

                    currentType = data[i];
                    currentCount = 1;
                }
            }

            encodedData.Add(currentType);
            encodedData.Add(currentCount);

            return encodedData.ToArray();
        }

        public static ushort[] Decode (ushort[] data) {

            List<ushort> decodedData = new List<ushort>();

            ushort type, count;

            for (int i = 0; i < data.Length; i += 2) {

                type = data[i]; count = data[i + 1];

                for (int ii = 0; ii < count; ++ii)
                    decodedData.Add(type);
            }

            return decodedData.ToArray();
        }
    }

    public static class CachedRunLengthEncoding {

        private static Mutex mutex = new Mutex();

        private static List<ushort> encodedData = new List<ushort>();

        public static ushort[] Encode (ushort[] data) {

            mutex.WaitOne(); try {

                encodedData.Clear();
                ushort currentType = data[0], currentCount = 1;

                for (int i = 1; i < data.Length; ++i) {

                    if (data[i] == currentType) {

                        currentCount++;

                    } else {

                        encodedData.Add(currentType);
                        encodedData.Add(currentCount);

                        currentType = data[i];
                        currentCount = 1;
                    }
                }

                encodedData.Add(currentType);
                encodedData.Add(currentCount);

                return encodedData.ToArray();

            } finally { mutex.ReleaseMutex(); }
        }

        private static List<ushort> decodedData = new List<ushort>();

        public static ushort[] Decode (ushort[] data) {

            mutex.WaitOne(); try {

                decodedData.Clear();

                ushort type, count;

                for (int i = 0; i < data.Length; i += 2) {

                    type = data[i]; count = data[i + 1];

                    for (int ii = 0; ii < count; ++ii)
                        decodedData.Add(type);
                }

                return decodedData.ToArray();

            } finally { mutex.ReleaseMutex(); }
        }
    }

    public static class GZipCompression {

        public static byte[] Compress (byte[] input) {

            using (MemoryStream memory = new MemoryStream()) {

                using (GZipStream zipStream = new GZipStream(memory, CompressionMode.Compress, true)) {

                    zipStream.Write(input, 0, input.Length);
                }

                return memory.ToArray();
            }
        }

        public static byte[] Decompress (byte[] input) {

            using (GZipStream zipStream = new GZipStream(new MemoryStream(input), CompressionMode.Decompress)) {

                const int size = 4096;
                byte[] buffer = new byte[size];

                using (MemoryStream memory = new MemoryStream()) {

                    int count = 0;

                    do {

                        count = zipStream.Read(buffer, 0, size);

                        if (count > 0) {

                            memory.Write(buffer, 0, count);
                        }

                    } while (count > 0);

                    return memory.ToArray();
                }
            }
        }
    }
}
