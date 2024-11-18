using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Renderer))]
public class WebImageReceiver : MonoBehaviour
{
    Material mat;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void UpdateMaterialMainTex(Texture2D mainTex)
    {
        mat.SetTexture("_MainTex", mainTex);
    }

}
