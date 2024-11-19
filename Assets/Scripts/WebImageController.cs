using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class WebImageController : MonoBehaviour
{
    private List<WebImageReceiver> webImages = new List<WebImageReceiver>();

    private void Start()
    {
        webImages = transform.GetComponentsInChildren<WebImageReceiver>().ToList();
    }

    public void DownloadImage(string location) => StartCoroutine(DownloadImage(ReceiveImage, location));

    
    private IEnumerator DownloadImage(Action<Texture2D> callback, string location)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(location);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }

    private void ReceiveImage(Texture2D texture)
    {
        foreach(WebImageReceiver webImage in webImages)
        {
            webImage.UpdateMaterialMainTex(texture);
        }
    }
}
