
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Structures;

namespace Qublock.ChunkLoading {

    public static class ChunkFetcher {

        private static Mutex mutex = new Mutex();

        private static List<ChunkLoc> fetchQueue = new List<ChunkLoc>();

        public static void FetchChunk (ChunkLoc loc) {

            fetchQueue.Add(loc);
        }
        public static ChunkLoc PopFetchQueue () {

            mutex.WaitOne(); try {

                ChunkLoc loc = fetchQueue[0];
                fetchQueue.RemoveAt(0);

                return loc;

            } finally { mutex.ReleaseMutex(); }
        }
        public static int FetchQueueCount () {

            mutex.WaitOne(); try {

                return fetchQueue.Count;

            } finally { mutex.ReleaseMutex(); }
        }

        private static List<FetchedChunk> fetchedChunks = new List<FetchedChunk>();

        public static void AddFetchedChunk (FetchedChunk fetchedChunk) {

            mutex.WaitOne(); try {

                fetchedChunks.Add(fetchedChunk);

            } finally { mutex.ReleaseMutex(); }
        }
        public static FetchedChunk PopFetchedChunk () {

            mutex.WaitOne(); try {

                FetchedChunk fetchedChunk = fetchedChunks[0];
                fetchedChunks.RemoveAt(0);

                return fetchedChunk;

            } finally { mutex.ReleaseMutex(); }
        }
        public static int FetchedCount () {

            mutex.WaitOne(); try {

                return fetchedChunks.Count;

            } finally { mutex.ReleaseMutex(); }
        }

        public static void ClearCache () {

            mutex.WaitOne(); try {

                fetchQueue.Clear();
                fetchedChunks.Clear();

            } finally { mutex.ReleaseMutex(); }
        }
    }

    public class FetchedChunk {

        public ChunkLoc loc;

        public ushort[] values;
    }
}
