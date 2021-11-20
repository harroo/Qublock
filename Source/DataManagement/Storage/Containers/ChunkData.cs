
using System;

using Qublock.Data.Storage.Structures;

namespace Qublock.Data.Storage.Containers {

    public class ChunkData {

        private ushort[] values = new ushort[32 * 32 * 32];

        public ushort this[int x, int y, int z] {

            get {

                return values[x * 32 * 32 + y * 32 + z];
            }

    	    set {

        		values[x * 32 * 32 + y * 32 + z] = value;
    	    }
        }

    	public ushort[] GetValues ()
    		=> values;

    	public bool valuesSet = false;

    	public void SetValues (ushort[] newValues) {

    		values = newValues;

    		valuesSet = true;
    	}

    	public bool Contains (ushort blockId)
            => Array.IndexOf(values, blockId) > -1;

        public ChunkData () {}

        public ChunkData (ushort[] initValues) {

            values = initValues;
        }

        public ChunkLoc location;
        public GridPos position;

        public ChunkData (ChunkLoc loc) {

            location = loc;
            position = new GridPos(loc.X * 32, loc.Y * 32, loc.Z * 32);
        }
    }
}
