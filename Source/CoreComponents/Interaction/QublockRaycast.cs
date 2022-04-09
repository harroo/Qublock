
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Storage.Containers;
using Qublock.Data.Blocks;
using Qublock.Core;
using Qublock.FloatingOrigin;

namespace Qublock.Core.Interaction {

    public class QublockRay {

        public static bool Fire (Vector3 position, Vector3 rotation, out RayResult result, float range) {

            return Fire(position, rotation, out result, range, 0.25f);
        }

        public static bool Fire (Vector3 position, Vector3 rotation, out RayResult result, float range, float jumpDistance) {

            float distanceTraveled = 0;

            position = Origin.UnityToOffset(position);

            GridCell previous = GridCell.Zero;

            while (distanceTraveled < range) {

                int x = Mathf.RoundToInt(position.x);
                int y = Mathf.RoundToInt(position.y);
                int z = Mathf.RoundToInt(position.z);
                ushort id = World.data[x, y, z];

                if (previous != new GridCell(x, y, z, id)) {

                    if (Argosy.Get(World.data[x, y, z]).Selectable) {

                        result = new RayResult {

                            hit = new GridCell(x, y, z, World.data[x, y, z]),
                            normal = previous
                        };
                        return true;
                    }

                    previous = new GridCell(x, y, z, id);
                }

                position += rotation * jumpDistance;
                distanceTraveled += jumpDistance;
            }

            result = new RayResult(GridCell.Zero, GridCell.Zero);
            return false;
        }
    }

    public struct RayResult {

        public GridCell hit, normal;

        public RayResult (GridCell a, GridCell b) {

            hit = a; normal = b;
        }
    }
}
