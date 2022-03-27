
using UnityEngine;

using System;
using System.Collections.Generic;

public class DynamicLightSimulation : MonoBehaviour {

    public static DynamicLightSimulation instance;
    private void Awake () { instance = this; }

    public List<Light> lightNodes = new List<Light>();

    private float timer = 4;
    private Vector3 playerPosCache;
    private int nodeCache;

    private void Update () {

        timer -= Time.deltaTime;
        if (timer < 0) timer = 1.28f; else return;

        if (
            playerPosCache == DynamicLightPlayerMonitor.position &&
            nodeCache == lightNodes.Count
        ) return;
        playerPosCache = DynamicLightPlayerMonitor.position;
        nodeCache = lightNodes.Count;

        Simulate();
    }

    public int lightMax = 16;

    public void LoadLevels () {

        // Load from settings, or similar, the maximum active lights.
    }

    private Dictionary<float, Light> lightMap = new Dictionary<float, Light>();
    private List<float> lightKeys = new List<float>();
    private float[] arr;
    private int index;

    private void Simulate () {

        // System.Diagnostics.Stopwatch stopWatch =
        //     new System.Diagnostics.Stopwatch();
        // stopWatch.Start();

        lightMap.Clear();
        lightKeys.Clear();

        foreach (Light light in lightNodes) {

            float distance = (DynamicLightPlayerMonitor.position - light.gameObject.transform.position).magnitude;

            lightMap.Add(distance, light);
            lightKeys.Add(distance);
        }

        arr = lightKeys.ToArray();
        Array.Sort(arr);

        index = 0;

        foreach (var val in arr) {

            // lightMap[val].transform.parent.gameObject.SetActive( index <= lightMax);
            if (DynamicLightManager.noLightSim)
                lightMap[val].enabled = false;
            else
                lightMap[val].enabled = index <= lightMax;

            index++;
        }

        // stopWatch.Stop();
        // Debug.Log(stopWatch.Elapsed.TotalMilliseconds);
    }
}
