using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramJitter : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    float _scanLineJitter = 0;
    // Color drift颜色偏移
    [SerializeField, Range(0, 1)]
    float _colorDrift = 0;
    [SerializeField]
    float _glitchSpeed = 2;
    Material mat;
    void Start()
    {
        //mat = new Material(Shader.Find("Custom/HologramBlock"));
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        var sl_thresh = Mathf.Clamp01(1.0f - _scanLineJitter * 1.2f);
        var sl_disp = 0.002f + Mathf.Pow(_scanLineJitter, 3) * 0.05f;
        var cd = new Vector2(_colorDrift * 0.04f, Time.time * 606.11f);
        mat.SetVector("_Params", new Vector4(sl_disp, sl_thresh, cd.x,cd.y));
        mat.SetFloat("_Speed", _glitchSpeed);
    }
}
