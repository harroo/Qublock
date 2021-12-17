
using UnityEngine;

namespace Qublock.FloatingOrigin {

    public static class Origin {

        public static int worldX, worldY, worldZ;

        public static int chunkX, chunkY, chunkZ;

        public static void AddToOffset (Vector3 newOffset) {

            worldX += (int)newOffset.x;
            worldY += (int)newOffset.y;
            worldZ += (int)newOffset.z;

            chunkX = worldX >> 5;
            chunkY = worldY >> 5;
            chunkZ = worldZ >> 5;
        }

        public static void ResetOffset () {

            worldX = 0; worldY = 0; worldZ = 0;

            chunkX = 0; chunkY = 0; chunkZ = 0;
        }

        public static Vector3 OffsetToUnity (Vector3 pos) {

            pos.x += worldX; pos.y += worldY; pos.z += worldZ;
            return pos;
        }

        public static Vector3 UnityToOffset (Vector3 pos) {

            pos.x -= worldX; pos.y -= worldY; pos.z -= worldZ;
            return pos;
        }
    }
}
