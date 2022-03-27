
using UnityEngine;

using Qublock.FloatingOrigin;

public class DynamicLightManager : MonoBehaviour {

    public static DynamicLightManager instance;
    private void Awake () { instance = this; }

    public Light[] lightsToSync;
    public Camera[] camerasToSync;

    [Space()]
    public float maxLightLevel = 0.6f;
    public float minUndergroundLightLevel = 0.1f;
    public float minSunlightLevel = 0.15f;

    [Space()]
    public bool enableSubZeroColor = true;
    public Color subZeroColor = Color.black;
    public float subZeroDarknessLevel = 0.0f;

    private float ambientLevel;
    private Color ambientColor;

    private void SetLightLevel (float level) {

        if (ambientLevel == level * maxLightLevel) return;

        ambientLevel = level * maxLightLevel;
    }

    private void SetLightColor (Color color) {

        if (ambientColor == color) return;

        ambientColor = color;
    }

    private bool underground, daytime;

    private void Start () {

        CalculateDynamicLights();
    }

    private int thicknessCache = 64;
    private float ambientLevelCache = 64.0f;
    private Color ambientColorCache = Color.green;

    private float PlayerHeight
        => DynamicLightPlayerMonitor.position.y - Origin.worldY;

    private void Update () {

        if (enableSubZeroColor && PlayerHeight <= 0 && !underground) {

            underground = true; CalculateDynamicLights(); return;
        }

        if (enableSubZeroColor && PlayerHeight > 0 && underground) {

            underground = false; CalculateDynamicLights(); return;
        }

        if (thicknessCache != DynamicLightPlayerMonitor.depthThickness) {

            thicknessCache = DynamicLightPlayerMonitor.depthThickness;
            CalculateDynamicLights(); return;
        }

        if (ambientLevelCache != ambientLevel) {

            ambientLevelCache = ambientLevel;
            CalculateDynamicLights(); return;
        }

        if (ambientColorCache != ambientColor) {

            ambientColorCache = ambientColor;
            CalculateDynamicLights(); return;
        }
    }

    private void CalculateDynamicLights () {

        // If the player is Sub-Zero, and 'tis enabled, set everything to
        // Sub-Zero and return.
        if (enableSubZeroColor && PlayerHeight <= 0) {

            foreach (var light in lightsToSync) {

                light.intensity = subZeroDarknessLevel;
                light.color = subZeroColor;
            }
            RenderSettings.ambientIntensity = subZeroDarknessLevel;

            FindObjectOfType<Welkien>().sky = subZeroColor;
            FindObjectOfType<Welkien>().ground = subZeroColor;
            // foreach (var cam in camerasToSync)
            //     cam.backgroundColor = subZeroColor;
            // RenderSettingsfogColor = subZeroColor;

            noLightSim = false;

        } else { // Otherwise, find the darkest light-value and use that.

            // Get the Depth-Level.
            float depth = DynamicLightPlayerMonitor.depthThickness;
            if (depth > 12) depth = 12;
            depth = 12 - depth;
            depth /= 12.0f;
            depth *= maxLightLevel;

            // If the Depth is darker.
            if (depth < ambientLevel + 0.1f) { // Plus 0.1f because sunlight is brighter.

                foreach (var light in lightsToSync) {

                    light.intensity = depth;
                    light.color = ambientColor;
                }
                RenderSettings.ambientIntensity
                    = depth / maxLightLevel * 0.82f + minUndergroundLightLevel;

                FindObjectOfType<Welkien>().sky = subZeroColor;
                FindObjectOfType<Welkien>().ground = subZeroColor;
                // foreach (var cam in camerasToSync)
                //     cam.backgroundColor = ambientColor;
                // RenderSettings.fogColor = ambientColor;

                noLightSim = depth > 0.56f;

            } else { // Otherwise, the sunlight is the darkest.

                foreach (var light in lightsToSync) {

                    light.intensity = ambientLevel;
                    light.color = ambientColor;
                }
                RenderSettings.ambientIntensity
                    = ambientLevel / maxLightLevel * 0.82f + minSunlightLevel;

                FindObjectOfType<Welkien>().sky = subZeroColor;
                FindObjectOfType<Welkien>().ground = subZeroColor;
                // foreach (var cam in camerasToSync)
                //     cam.backgroundColor = ambientColor;
                // RenderSettingsfogColor = ambientColor;

                noLightSim = ambientLevel > 0.56f;
            }
        }
    }

    public static bool noLightSim = false;

    private Color tarColor = Color.black;
    private float tarLevel = 64.0f;

    public static void SetLight (float level) {

        instance.tarLevel = level;
        instance.SetLightLevel(level);
    }
    public static void SetColor (Color color) {

        instance.tarColor = color;
        instance.SetLightColor(color);
    }
    public static float GetLightLevel () {

        return instance.tarLevel;
    }
    public static Color GetLightColor () {

        return instance.tarColor;
    }
    public static bool isDay {
        get { return instance.daytime; }
        set { instance.daytime = value; }
    }

    public static bool InGround ()
        => instance.PlayerHeight <= 0 || DynamicLightPlayerMonitor.depthThickness > 3;
}
