using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class WebImageController : MonoBehaviour
{
    private struct WeatherURL
    {
        public string imageURL;
        public string weatherDataURL;
    }

    private List<WebImageReceiver> webImages = new List<WebImageReceiver>();

    // private const string florida = "https://www.publicdomainpictures.net/pictures/90000/nahled/sunset-key-west-florida.jpg";
    
    private const string germany = "https://www.goodfreephotos.com/albums/germany/other-germany/great-landscape-of-the-valley-in-germany.jpg";

    private const string madagascar = "https://upload.wikimedia.org/wikipedia/commons/0/0a/Sunset_baobabs_Madagascar.jpg";

    private const string mongolia = "https://www.worldhistory.org/uploads/images/11333.jpg?v=1599178503-0";

    private const string australia = "https://dri.es/files/cache/australia-2024/avalon-beach-1920w.jpg";

    private WeatherURL florida;

    private void Start()
    {
        webImages = transform.GetComponentsInChildren<WebImageReceiver>().ToList();

        florida = new WeatherURL();
        florida.imageURL = "https://www.publicdomainpictures.net/pictures/90000/nahled/sunset-key-west-florida.jpg";
        florida.weatherDataURL = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&appid=42f69487cb244eaf525fe22a087d99e5";
    }

    public void DownloadImage(string location) => StartCoroutine(DownloadImage(ReceiveImage, nameof(location)));

    
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
