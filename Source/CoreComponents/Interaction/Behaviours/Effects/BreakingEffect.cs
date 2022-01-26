
using UnityEngine;

using Qublock.FloatingOrigin;
using Qublock.Core;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class BreakingEffect : MonoBehaviour {

    public Texture2D[] frames;

    private MeshRenderer meshRenderer;

    private void Start () {

        meshRenderer = GetComponent<MeshRenderer>();

        Hide();
    }

    public void Hide () {

        meshRenderer.enabled = false;
    }

    public void Show () {

        meshRenderer.enabled = true;
    }

    public void SetPos (Vector3 pos, Vector3 plpos) {

        transform.position = pos;
    }

    public void SetFrame (float percent) { try {

        meshRenderer.material.mainTexture = frames[(frames.Length - 2) - Mathf.RoundToInt(percent * (frames.Length - 2))];

    } catch {} }
}
