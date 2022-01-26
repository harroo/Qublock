
using System;

namespace Qublock.Data.Storage.Structures {

    [Serializable]
    public struct GridCell : IEquatable<GridCell> {

        public int x;
        public int y;
        public int z;

        public ushort id;

        public GridCell (int x, int y, int z, ushort id) {

            this.x = x; this.y = y; this.z = z; this.id = id;
        }

        public static GridCell Zero => new GridCell(0, 0, 0, 0);

        #region Equality members
        public bool Equals (GridCell other) {
            return x == other.x && y == other.y && z == other.z && id == other.id;
        }
        public bool Equals (GridPos gridPos) {
            return x == gridPos.x && y == gridPos.y && z == gridPos.z;
        }
        public override bool Equals (object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridCell other && Equals(other);
        }
        public override int GetHashCode () {
            unchecked {
                var hashCode = x + id;
                hashCode = (hashCode * 397) ^ y;
                hashCode = (hashCode * 397) ^ z;
                return hashCode;
            }
        }
        public static bool operator == (GridCell left, GridCell right) {
            return left.Equals(right);
        }
        public static bool operator != (GridCell left, GridCell right) {
            return !left.Equals(right);
        }
        #endregion
    }
}
