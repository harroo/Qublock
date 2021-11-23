
using System;
using System.Linq;
using System.Collections.Generic;

using Qublock.Data.Storage.Containers;
using Qublock.Data.Storage.Structures;
using Qublock.Data.Serialization;
using Qublock.ProceduralGeneration;

namespace Qublock.Data.Managers {

    public static class RegionManager {

        private static Dictionary<RegionLoc, RegionData> regions
            = new Dictionary<RegionLoc, RegionData>();


        public static bool RegionLoaded (RegionLoc loc)
            => regions.ContainsKey(loc);

        public static bool RegionExists (RegionLoc loc)
            => Serializer.RegionFileExists(loc);

        public static List<RegionLoc> GetRegionLocs ()
            => (from kvp in regions select kvp.Key).ToList();

        public static ChunkData[] GetRegionData (RegionLoc loc)
            => regions[loc].GetChunks();

        public static int RegionCount ()
            => regions.Count;


        private static RegionData LoadRegion (RegionLoc loc) {

            return Serializer.Deserialize(loc);
        }

        private static void SaveRegion (RegionLoc loc) {

            if (!regions.ContainsKey(loc)) return;

            Serializer.Serialize(regions[loc]);
        }

        private static void GenerateRegion (RegionData region) {

            ChunkData[] chunks = new ChunkData[8 * 8 * 8];

            int index = 0;

            RegionLoc regionChunkLoc = new RegionLoc(
                region.location.X * 8,
                region.location.Y * 8,
                region.location.Z * 8
            );

            for (int x = 0; x < 8; ++x)
                for (int y = 0; y < 8; ++y)
                    for (int z = 0; z < 8; ++z) {

                        chunks[index] = new ChunkData(new ChunkLoc(
                            regionChunkLoc.X + x,
                            regionChunkLoc.Y + y,
                            regionChunkLoc.Z + z
                        ));

                        ProceduralGenerator.GenerateChunkData(chunks[index]);

                        index++;
                    }

            region.SetChunks(chunks);
        }


        public static ushort[] GetChunkValues (RegionLoc rloc, ChunkLoc localCloc) {

            if (!regions.ContainsKey(rloc)) return null;

            return regions[rloc][localCloc.X, localCloc.Y, localCloc.Z].GetValues();
        }

        public static void SetRegionValue (RegionLoc rloc, ChunkLoc localCloc, int x, int y, int z, ushort id) {

            if (!regions.ContainsKey(rloc)) return;

            regions[rloc][localCloc.X, localCloc.Y, localCloc.Z][x, y, z] = id;

            FlagRegionForSaving(rloc);
        }

        public static ushort GetRegionValue (RegionLoc rloc, ChunkLoc localCloc, int x, int y, int z) {

            if (!regions.ContainsKey(rloc)) return 0;

            return regions[rloc][localCloc.X, localCloc.Y, localCloc.Z][x, y, z];
        }


        public static void EnsureRegion (RegionLoc loc) {

            if (RegionLoaded(loc)) return;

            if (RegionExists(loc)) {

                regions.Add(loc, LoadRegion(loc));

            } else {

                RegionData region = new RegionData(loc);
                GenerateRegion(region);

                regions.Add(loc, region);

                FlagRegionForSaving(loc);
            }
        }


        private static List<RegionLoc> unsavedRegions
            = new List<RegionLoc>();


        public static int UnsavedCount ()
            => unsavedRegions.Count;

        public static bool RegionUnsaved (RegionLoc loc)
            => regions.ContainsKey(loc) ? false : unsavedRegions.Contains(loc);

        public static bool UnsavedChanges ()
            => unsavedRegions.Count != 0;


        public static void SaveAllUnsaved () {

            while (unsavedRegions.Count != 0) {

                SaveRegion(unsavedRegions[0]);

                unsavedRegions.RemoveAt(0);
            }
        }

        public static void FlagRegionForSaving (RegionLoc loc) {

            if (unsavedRegions.Contains(loc)) return;

            unsavedRegions.Add(loc);
        }

        public static void UnloadAllRegions () {

            regions.Clear();
            unsavedRegions.Clear();
        }
    }
}
