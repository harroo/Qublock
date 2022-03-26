
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

            RaycastHit hit;
            Ray sray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            if (Physics.Raycast(sray, out hit, raycastRange)) {

                if (hit.collider.tag == "Entity Block") {

                    hit.collider.GetComponent<EntityBlockBehaviour>().OnLeftClick(); return;
                }
            }

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, raycastRange)) {

                World.EditBlock(ray.hit.x, ray.hit.y, ray.hit.z, 0);
            }
        }

        if (Input.GetMouseButtonDown(1)) {

            RaycastHit hit;
            Ray sray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            if (Physics.Raycast(sray, out hit, raycastRange)) {

                if (hit.collider.tag == "Entity Block") {

                    hit.collider.GetComponent<EntityBlockBehaviour>().OnRightClick(); return;
                }
            }

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, raycastRange)) {

                World.EditBlock(ray.normal.x, ray.normal.y, ray.normal.z, 4);
            }
        }
    }
}
