
using System;

namespace Qublock.Data.Storage.Structures {

    [Serializable]
    public struct ChunkLoc : IEquatable<ChunkLoc> {

        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public ChunkLoc (int x, int y, int z) {

            X = x; Y = y; Z = z;
        }

        public static ChunkLoc FromWorldPos (int x, int y, int z) {

            return new ChunkLoc (x >> 5, y >> 5, z >> 5);
        }

        #region Equality members
        public bool Equals (ChunkLoc other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        public override bool Equals (object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ChunkLoc other && Equals(other);
        }
        public override int GetHashCode () {
            unchecked {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }
        public static bool operator == (ChunkLoc left, ChunkLoc right) {
            return left.Equals(right);
        }
        public static bool operator != (ChunkLoc left, ChunkLoc right) {
            return !left.Equals(right);
        }
        #endregion
    }
}
