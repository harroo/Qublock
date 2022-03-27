
using UnityEngine;

public class DynamicLightIndexer : MonoBehaviour {

    private void Start () {

        DynamicLightSimulation.instance.lightNodes.Add(GetComponent<Light>());
    }

    private void OnDestroy () {

        DynamicLightSimulation.instance.lightNodes.Remove(GetComponent<Light>());
    }
}
