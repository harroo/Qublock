
using UnityEngine;

public class SyncToOrigin : MonoBehaviour {

    private void Start () {

        OriginSync.instance.syncCache.Add(transform);
    }

    private void OnDestroy () {

        OriginSync.instance.syncCache.Remove(transform);
    }
}
