
using UnityEngine;

using System;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.Core.ThreadMesher;
using Qublock.Core;

public class ThreadMesher : MonoBehaviour {

    private void Start () {

        ThreadMeshing.shouldStop = false;

        new Thread(()=>ThreadMeshing.MeshLoop()).Start();
    }

    private void Update () {

        while (ThreadMeshing.GetCountOut() != 0) {

            ChunkLoc loc = ThreadMeshing.PopOut();

            if (World.ChunkLoaded(loc))
                World.data.chunks[loc].Asign();
        }
    }

    private void OnDestroy () {

        ThreadMeshing.shouldStop = true;
    }
}

namespace Qublock.Core.ThreadMesher {

    public static class ThreadMeshing {

        private static Mutex mutex = new Mutex();

        private static List<Chunk> meshQueue = new List<Chunk>();

        public static int GetCount () {

            mutex.WaitOne(); int a = 0; try { a = meshQueue.Count; }
            finally { mutex.ReleaseMutex(); } return a;
        }

        public static void Enqueue (Chunk chunk) {

            mutex.WaitOne(); try { if (!meshQueue.Contains(chunk)) { meshQueue.Add(chunk); } }
            finally { mutex.ReleaseMutex(); }

            outMutex.WaitOne(); try { if (outList.Contains(chunk.location)) { outList.Remove(chunk.location); } }
            finally { outMutex.ReleaseMutex(); }
        }


        public static Chunk Palm () {

            mutex.WaitOne(); Chunk a; try { a = meshQueue[0]; }
            finally { mutex.ReleaseMutex(); } return a;
        }

        public static void Slice () {

            mutex.WaitOne(); try { meshQueue.RemoveAt(0); }
            finally { mutex.ReleaseMutex(); }
        }

        public static bool shouldStop = false;

        public static void MeshLoop () {

            while (!shouldStop) { try {

                if (GetCount() != 0) {

                    Chunk chunk = Palm();

                    if (!World.ChunkLoaded(chunk.location))
                        { Slice(); continue; }

                    chunk.Render();

                    AddOut(chunk.location);
                    Slice();

                } else Thread.Sleep(4);

                } catch (Exception ex) { Debug.LogWarning("ThreadMeshing: " + ex.Message); }
            }

            Debug.Log("ThreadMesher is offline..");
        }

        private static Mutex outMutex = new Mutex();

        private static List<ChunkLoc> outList = new List<ChunkLoc>();

        public static int GetCountOut () {

            outMutex.WaitOne(); int a = 0; try { a = outList.Count; }
            finally { outMutex.ReleaseMutex(); } return a;
        }

        public static void AddOut (ChunkLoc loc) {

            outMutex.WaitOne(); try { if (!outList.Contains(loc)) { outList.Add(loc); } }
            finally { outMutex.ReleaseMutex(); }
        }

        public static ChunkLoc PopOut () {

            outMutex.WaitOne(); ChunkLoc a; try { a = outList[0]; outList.RemoveAt(0); }
            finally { outMutex.ReleaseMutex(); } return a;
        }
    }
}
