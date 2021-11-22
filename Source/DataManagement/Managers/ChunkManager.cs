
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Containers;
using Qublock.Data.Storage.Structures;

namespace Qublock.Data.Managers {

    public static class ChunkManager {

        public static Mutex mutex = new Mutex();

        public static ushort[] GetChunkValues (ChunkLoc loc) {

            ushort[] chunkValues;

            mutex.WaitOne(); try {

                RegionLoc regionLoc = RegionLoc.FromChunkPos(loc);
                ChunkLoc localChunkLoc = new ChunkLoc(
                    loc.X & 0x7, loc.Y & 0x7, loc.Z & 0x7
                );

                RegionManager.EnsureRegion(regionLoc);

                chunkValues = RegionManager.GetChunkValues(regionLoc, localChunkLoc);

            } finally { mutex.ReleaseMutex(); }

            return chunkValues;
        }

        public static bool ChunkLoaded (ChunkLoc loc) {

            bool chunkLoaded = false;

            mutex.WaitOne(); try {

                RegionLoc regionLoc = RegionLoc.FromChunkPos(loc);

                chunkLoaded = RegionManager.RegionLoaded(regionLoc);

            } finally { mutex.ReleaseMutex(); }

            return chunkLoaded;
        }

        public static bool ChunkExists (ChunkLoc loc) {

            bool chunkExists = false;

            mutex.WaitOne(); try {

                RegionLoc regionLoc = RegionLoc.FromChunkPos(loc);

                chunkExists = RegionManager.RegionExists(regionLoc);

            } finally { mutex.ReleaseMutex(); }

            return chunkExists;
        }

        public static void EnsureChunk (ChunkLoc loc) {

            mutex.WaitOne(); try {

                RegionLoc regionLoc = RegionLoc.FromChunkPos(loc);

                RegionManager.EnsureRegion(regionLoc);

            } finally { mutex.ReleaseMutex(); }
        }

        public static void SetWorldValue (int x, int y, int z, ushort id) {

            mutex.WaitOne(); try {

                ChunkLoc chunkLoc = ChunkLoc.FromWorldPos(x, y, z);
                RegionLoc regionLoc = RegionLoc.FromWorldPos(x, y, z);
                ChunkLoc localChunkLoc = new ChunkLoc(
                    chunkLoc.X & 0x7, chunkLoc.Y & 0x7, chunkLoc.Z & 0x7
                );

                RegionManager.EnsureRegion(regionLoc);

                RegionManager.SetRegionValue(regionLoc, localChunkLoc, x & 0x1F, y & 0x1F, z & 0x1F, id);

            } finally { mutex.ReleaseMutex(); }
        }

        public static ushort GetWorldValue (int x, int y, int z, bool ensure = false) {

            ushort worldValue = 0;

            mutex.WaitOne(); try {

                ChunkLoc chunkLoc = ChunkLoc.FromWorldPos(x, y, z);
                RegionLoc regionLoc = RegionLoc.FromWorldPos(x, y, z);
                ChunkLoc localChunkLoc = new ChunkLoc(
                    chunkLoc.X & 0x7, chunkLoc.Y & 0x7, chunkLoc.Z & 0x7
                );

                if (ensure) RegionManager.EnsureRegion(regionLoc);

                worldValue = RegionManager.GetRegionValue(regionLoc, localChunkLoc, x & 0x1F, y & 0x1F, z & 0x1F);

            } finally { mutex.ReleaseMutex(); }

            return worldValue;
        }

        public static int GetRegionCount () {

            int regionCount = 0;

            mutex.WaitOne(); try {

                regionCount = RegionManager.RegionCount();

            } finally { mutex.ReleaseMutex(); }

            return regionCount;
        }

        public static int GetUnsavedCount () {

            int unsavedCount = 0;

            mutex.WaitOne(); try {

                unsavedCount = RegionManager.UnsavedCount();

            } finally { mutex.ReleaseMutex(); }

            return unsavedCount;
        }

        public static void SaveAll () {

            mutex.WaitOne(); try {

                if (RegionManager.UnsavedChanges())
                    RegionManager.SaveAllUnsaved();

            } finally { mutex.ReleaseMutex(); }
        }

        public static void CollectGarbage () {

            mutex.WaitOne(); try {

                if (RegionManager.UnsavedChanges())
                    RegionManager.SaveAllUnsaved();

                RegionManager.UnloadAllRegions();

            } finally { mutex.ReleaseMutex(); }
        }

        public static List<RegionLoc> GetRegionLocList () {

            List<RegionLoc> list = new List<RegionLoc>();

            mutex.WaitOne(); try {

                list = RegionManager.GetRegionLocs();

            } finally { mutex.ReleaseMutex(); }

            return list;
        }

        public static ChunkData[] GetRegionData (RegionLoc loc) {

            ChunkData[] list;

            mutex.WaitOne(); try {

                list = RegionManager.GetRegionData(loc);

            } finally { mutex.ReleaseMutex(); }

            return list;
        }
    }
}
