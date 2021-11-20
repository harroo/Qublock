
using System;

namespace Qublock.Data.Storage.Structures {

    [Serializable]
    public struct GridPos : IEquatable<GridPos> {

        public int x;
        public int y;
        public int z;

        public GridPos (int x, int y, int z) {

            this.x = x; this.y = y; this.z = z;
        }

        #region Equality members
        public bool Equals (GridPos other) {
            return x == other.x && y == other.y && z == other.z;
        }
        public override bool Equals (object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridPos other && Equals(other);
        }
        public override int GetHashCode () {
            unchecked {
                var hashCode = x;
                hashCode = (hashCode * 397) ^ y;
                hashCode = (hashCode * 397) ^ z;
                return hashCode;
            }
        }
        public static bool operator == (GridPos left, GridPos right) {
            return left.Equals(right);
        }
        public static bool operator != (GridPos left, GridPos right) {
            return !left.Equals(right);
        }
        #endregion
    }
}
