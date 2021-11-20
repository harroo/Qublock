
using System;

namespace Qublock.Data.Storage.Structures {

    [Serializable]
    public struct RegionLoc : IEquatable<RegionLoc> {

        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public RegionLoc (int x, int y, int z) {

            X = x; Y = y; Z = z;
        }

        public static RegionLoc FromWorldPos (int x, int y, int z) {

            return new RegionLoc (x >> 8, y >> 8, z >> 8);
        }

        public static RegionLoc FromChunkPos (ChunkLoc loc) {

            return new RegionLoc (loc.X >> 3, loc.Y >> 3, loc.Z >> 3);
        }

        #region Equality members
        public bool Equals (RegionLoc other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        public override bool Equals (object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RegionLoc other && Equals(other);
        }
        public override int GetHashCode () {
            unchecked {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }
        public static bool operator == (RegionLoc left, RegionLoc right) {
            return left.Equals(right);
        }
        public static bool operator != (RegionLoc left, RegionLoc right) {
            return !left.Equals(right);
        }
        #endregion
    }
}
