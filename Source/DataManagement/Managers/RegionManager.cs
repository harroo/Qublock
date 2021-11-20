
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Qublock.Data.Storage.Containers;
using Qublock.Data.Storage.Structures;

namespace Qublock.Data.Managers {

    public static class RegionManager {

        private static Dictionary<RegionLoc, RegionData> regions
            = new Dictionary<RegionLoc, RegionData>();

        private static List<RegionLoc> unsavedRegions
            = new List<RegionLoc>();


        public static bool RegionLoaded (RegionLoc loc)
            => regions.ContainsKey(loc);

        public static bool RegionExists (RegionLoc loc)
            => false;
            // serialization call

        public static List<RegionLoc> GetRegionLocs ()
            => (from kvp in regions select kvp.Key).ToList();

        public static ChunkData[] GetRegionData (RegionLoc loc)
            => regions[loc].GetChunks();

        public static int RegionCount ()
            => regions.Count;


        public static int UnsavedCount ()
            => unsavedRegions.Count;
    }
}
