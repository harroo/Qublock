
using UnityEngine;

using Qublock.Core.Interaction;
using Qublock.FloatingOrigin;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class SelectorEffect : MonoBehaviour {

    public float range;

    public Transform cam;

    private bool hide = false;

    private void Update () {

        if (hide) return;

        RayResult ray;

        if (QublockRay.Fire(cam.position, cam.forward, out ray, range)) {

            transform.position = Origin.OffsetToUnity(new Vector3(ray.hit.x, ray.hit.y, ray.hit.z));

        } else {

            transform.position = new Vector3(0, -169, 0);
        }
    }

    private void LateUpdate () {

        if (Cursor.visible) return;

        if (Input.GetKeyDown(KeyCode.H)) {

            hide = !hide;

            if (hide) transform.position = new Vector3 (

                UnityEngine.Random.Range(-9999, 9999),
                UnityEngine.Random.Range(-9999, 9999),
                UnityEngine.Random.Range(-9999, 9999)
            );
        }
    }
}
