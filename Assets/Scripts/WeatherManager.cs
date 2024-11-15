using EditorAttributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager : MonoBehaviour
{
    //find weather by City
    private const string florida = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&appid=42f69487cb244eaf525fe22a087d99e5";
    
    private const string germany = "http://api.openweathermap.org/data/2.5/weather?q=Heinsberg,de&appid=42f69487cb244eaf525fe22a087d99e5";

    private const string madagascar = "http://api.openweathermap.org/data/2.5/weather?q=Hell-Ville,mg&appid=42f69487cb244eaf525fe22a087d99e5";

    private const string mongolia = "http://api.openweathermap.org/data/2.5/weather?q=Onon,mn&appid=42f69487cb244eaf525fe22a087d99e5";

    private const string australia = "http://api.openweathermap.org/data/2.5/weather?q=Yass,au&appid=42f69487cb244eaf525fe22a087d99e5";

    [SerializeField] private List<Material> skyboxMats = new List<Material>();

    
    [Header("Select City for Skybox")]
    [SerializeField] private bool toggleButtons;
    
    [Button(nameof(toggleButtons), ConditionResult.EnableDisable, true)]
    public void Check_Florida() => CallWeather(florida);
    [Button(nameof(toggleButtons), ConditionResult.EnableDisable, true)]
    public void Check_Germany() => CallWeather(germany);
    [Button(nameof(toggleButtons), ConditionResult.EnableDisable, true)]
    public void Check_Madagascar() => CallWeather(madagascar);
    [Button(nameof(toggleButtons), ConditionResult.EnableDisable, true)]
    public void Check_Mongolia() => CallWeather(mongolia);
    [Button(nameof(toggleButtons), ConditionResult.EnableDisable, true)]
    public void Check_Australia() => CallWeather(australia);


    //[Button("Find Weather in Florida", 30f)]
    //public void CallWeather1() => CallWeather(florida);

    //[Button("Find Weather in Germany", 30f)]
    //public void CallWeather2() => CallWeather(germany);

    //[Button("Find Weather in Madagascar", 30f)]
    //public void CallWeather3() => CallWeather(madagascar);

    //[Button("Find Weather in Mongolia", 30f)]
    //public void CallWeather4() => CallWeather(mongolia);

    //[Button("Find Weather in Australia", 30f)]
    //public void CallWeather5() => CallWeather(australia);

    

    private void Start()
    {
        //StartCoroutine(GetWeatherXML(florida, OnXMLDataLoaded));
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

    public IEnumerator GetWeatherXML(string location, Action<string> callback)
    {
        return CallAPI(location, callback);
    }

    public void CallWeather(string location)
    {
        StartCoroutine(GetWeatherXML(location, OnXMLDataLoaded));
    }

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);
        
        WeatherLocation location = JsonConvert.DeserializeObject<WeatherLocation>(data);

        print(DateTime.UtcNow.AddSeconds(location.Timezone));
    }


    

}
