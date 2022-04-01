
//this works well, but it requires exact replicars for svbasemeshing

using UnityEngine;

using Qublock.Core;
using Qublock.Core.Interaction;
using Qublock.Data.Blocks;
using Qublock.FloatingOrigin;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class AdvancedSelectorEffect : MonoBehaviour {

    public float range;
    public Transform cam;

    private bool hide = false;
    private Vector3 posCache;

    private Mesh mesh;

    private void Start () {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update () {

        if (hide) return;

        RayResult ray;

        if (QublockRay.Fire(cam.position, cam.forward, out ray, range)) {

            posCache = Origin.OffsetToUnity(new Vector3(ray.hit.x, ray.hit.y, ray.hit.z));

            if (transform.position != posCache) { transform.position = posCache;

                Argosy.Get(World.data[ray.hit.x, ray.hit.y, ray.hit.z]).SingleVoxelDraw(mesh);
            }

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
