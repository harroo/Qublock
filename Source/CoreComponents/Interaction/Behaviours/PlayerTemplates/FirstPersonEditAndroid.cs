
using UnityEngine;

using Qublock.Data.Storage.Structures;
using Qublock.Data.Blocks;
using Qublock.Core.Interaction;
using Qublock.Core;
using Qublock.FloatingOrigin;

public class FirstPersonEditAndroid : MonoBehaviour {

    public float range;

    private float interactionDelay = 0;

    private float digTemp, digTimer;
    private GridCell digPlace;
    private bool canBreakCache;

    public BreakingEffect effect;

    private EntityBlockBehaviour hitCache;

    private void Update () {

        // if (Cursor.visible) return;

        if (digTimer > 0) {

            if (LeftRelease()) {

                digTimer = 0; effect.Hide(); return;
            }

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, range)) {

                if (ray.hit != digPlace) {

                    digTimer = 0; effect.Hide(); return;
                }

            } else {

                digTimer = 0; effect.Hide(); return;
            }

            digTimer -= Time.deltaTime * (canBreakCache ? 1.8f : 0.5f);
            effect.SetFrame(digTimer / digTemp);

            if (digTimer <= 0) {

                World.EditBlock(digPlace.x, digPlace.y, digPlace.z, 0);
                effect.Hide();
            }

        } else if (LeftClick()) {

            RaycastHit hit;
            Ray sray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            if (Physics.Raycast(sray, out hit, range)) {

                if (hit.collider.tag == "Entity Block") {

                    EntityBlockBehaviour behaviour = hit.collider.GetComponent<EntityBlockBehaviour>();

                    if (hitCache == behaviour) return;

                    hitCache = behaviour;
                    hitCache.OnLeftClick(); return;
                }
            }

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, range)) {

                digPlace = ray.hit;

                digTemp = 1.0f; //time to dig the block
                digTimer = digTemp;

                effect.SetPos(
                    Origin.OffsetToUnity(new Vector3(ray.hit.x, ray.hit.y, ray.hit.z)),
                    Origin.OffsetToUnity(new Vector3(ray.normal.x, ray.normal.y, ray.normal.z))
                );
                effect.SetFrame(digTimer / digTemp);
                effect.Show();

                canBreakCache = true; //if the tool can collect the block
            }
        }

        if (LeftRelease()) hitCache = null;

        interactionDelay -= Time.deltaTime;
        if (RightRelease()) interactionDelay = 0.0f;

        if (RightClick() && interactionDelay < 0.0f) {

            interactionDelay = 0.32f;

            RaycastHit hit;
            Ray sray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            if (Physics.Raycast(sray, out hit, range)) {

                if (hit.collider.tag == "Entity Block") {

                    hit.collider.GetComponent<EntityBlockBehaviour>().OnRightClick(); return;
                }
            }

            RayResult ray;
            if (QublockRay.Fire(transform.position, transform.forward, out ray, range)) {

                Vector3 placePos = Origin.OffsetToUnity(new Vector3(ray.normal.x, ray.normal.y, ray.normal.z));
                if (placePos == new Vector3(
                        Mathf.RoundToInt(transform.position.x),
                        Mathf.RoundToInt(transform.position.y - 0.5f),
                        Mathf.RoundToInt(transform.position.z))
                    ||
                    placePos == new Vector3(
                        Mathf.RoundToInt(transform.position.x),
                        Mathf.RoundToInt(transform.position.y + 0.5f),
                        Mathf.RoundToInt(transform.position.z))
                ) return;

                World.EditBlock(ray.normal.x, ray.normal.y, ray.normal.z, 444);
            }
        }
    }

    private bool leftClick, rightClick;
    private bool leftClickCap, rightClickCap;

    private bool LeftClick () { return leftClick || (!Cursor.visible && Input.GetMouseButton(0)); }
    private bool RightClick () { return rightClick || (!Cursor.visible && Input.GetMouseButton(1)); }

    private bool LeftRelease () {

        if (!leftClick && leftClickCap) {

            leftClickCap = false; return true;
        }
        return !Cursor.visible && Input.GetMouseButtonUp(0);
    }
    private bool RightRelease () {

        if (!rightClick && rightClickCap) {

            rightClickCap = false; return true;
        }
        return !Cursor.visible && Input.GetMouseButtonUp(1);
    }

    public void LeftClick_Down () { leftClick = true; }
    public void LeftClick_Up () { leftClick = false; leftClickCap = true; }

    public void RightClick_Down () { rightClick = true; }
    public void RightClick_Up () { rightClick = false; rightClickCap = true; }
}
