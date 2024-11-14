using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class WeatherManager : MonoBehaviour
{
    //find weather by City
    private const string florida = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=42f69487cb244eaf525fe22a087d99e5";

    //find weather by ZIP code (doesn't work for all countries)
    private const string germany = "https://api.openweathermap.org/data/2.5/weather?zip=52525,de&appid=42f69487cb244eaf525fe22a087d99e5";
    
    //find weather by Longitute/Latitude
    private const string madagascar = "https://api.openweathermap.org/data/2.5/weather?lat=-13.397826&lon=48.266638&appid=42f69487cb244eaf525fe22a087d99e5";

    //find weather by city code (weathermap support)
    private const string mongolia = "https://api.openweathermap.org/data/2.5/weather?id=2028461&appid=42f69487cb244eaf525fe22a087d99e5";

    private const string australia = "http://api.openweathermap.org/data/2.5/weather?q=Yass,au&mode=xml&appid=42f69487cb244eaf525fe22a087d99e5";

    private void Start()
    {
        StartCoroutine(GetWeatherXML(OnXMLDataLoaded));
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(australia, callback);
    }

    

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);
    }


}
