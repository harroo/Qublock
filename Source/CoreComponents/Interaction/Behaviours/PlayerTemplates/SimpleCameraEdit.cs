
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Blocks;
using Qublock.Core.Interaction;
using Qublock.Core;

public class SimpleCameraEdit : MonoBehaviour {

    public int raycastRange = 4;

    private void Update () {

        if (Cursor.visible) return;

        if (Input.GetMouseButtonDown(0)) {

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, raycastRange)) {

                World.EditBlock(ray.hit.x, ray.hit.y, ray.hit.z, 0);
            }
        }

        if (Input.GetMouseButtonDown(1)) {

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, raycastRange)) {

                World.EditBlock(ray.normal.x, ray.normal.y, ray.normal.z, 4);
            }
        }
    }
}
