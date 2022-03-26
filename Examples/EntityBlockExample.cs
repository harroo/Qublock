
using UnityEngine;

public class EntityBlockExample : EntityBlockBehaviour {

    public override void OnRightClick () {

        Debug.Log("right click");
    }

    public override void OnLeftClick () {

        Debug.Log("left click");
    }

    private void Start () {

        Debug.Log("start");
    }
}
