
using UnityEngine;

using Qublock.Core;
using Qublock.Data.Blocks;
using Qublock.Data.Storage.Structures;
using Qublock.FloatingOrigin;

public class DynamicLightPlayerMonitor : MonoBehaviour {

    private static DynamicLightPlayerMonitor instance;
    private void Awake () { instance = this; }

    public float scanDelay;

    private float timer = 4;

    private void Start () {

        depthThickness = 0;
    }

    private void Update () {

        timer -= 1 * Time.deltaTime;

        if (timer < 0) {

            timer = scanDelay;

            PerformScan();
        }
    }

    private GridPos lastPos;

    public static int depthThickness;
    public static Vector3 position => instance.transform.position;

    public void PerformScan () {

        // Get the current player's position.
        GridPos currentPos = new GridPos(
            Mathf.RoundToInt(transform.position.x) - Origin.worldX,
            Mathf.RoundToInt(transform.position.y - .5f) - Origin.worldY,
            Mathf.RoundToInt(transform.position.z) - Origin.worldZ
        );

        // Check if the player has moved since the last scan.
        if (lastPos == currentPos) return;
        lastPos = currentPos;

        depthThickness = 0;

        // Scan upwards 64 blocks.
        for (int y = 0; y < 64; ++y) {

            //for some reason i thought opaque means transparent so assume opaque means transparent
            if (Argosy.Get(World.data[currentPos.x, currentPos.y + y, currentPos.z]).Solid)
                depthThickness++;
        }
    }
}
