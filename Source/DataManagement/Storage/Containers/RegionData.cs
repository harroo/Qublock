
using System;

using Qublock.Data.Storage.Structures;

namespace Qublock.Data.Storage.Containers {

    public class RegionData {

        private ChunkData[] chunks = new ChunkData[8 * 8 * 8];

        public ChunkData this[int x, int y, int z] {

            get {

                return chunks[x * 8 * 8 + y * 8 + z];
            }

            set {

                chunks[x * 8 * 8 + y * 8 + z] = value;
            }
        }

        public ChunkData[] GetChunks ()
            => chunks;

        public void SetChunks (ChunkData[] data) {

            chunks = data;
        }

        public RegionLoc location;
        public GridPos position;

        public RegionData (RegionLoc loc) {

            location = loc;
            position = new GridPos(loc.X * 256, loc.Y * 256, loc.Z * 256);
        }
    }
}
