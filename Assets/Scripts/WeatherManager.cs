using EditorAttributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private WebImageController webImageController;
    [SerializeField] private List<WeatherSO> weatherTypes;
    [SerializeField] private Light sun;

    [SerializeField] private LocationSO florida;
    [SerializeField] private LocationSO germany;
    [SerializeField] private LocationSO madagascar;
    [SerializeField] private LocationSO mongolia;
    [SerializeField] private LocationSO australia;

    
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

    public void CallWeather(LocationSO location)
    {
        StartCoroutine(GetWeatherXML(location.weatherDataURL, OnXMLDataLoaded));
        webImageController.DownloadImage(location.imageURL);
    }

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);
        
        WeatherLocation location = JsonConvert.DeserializeObject<WeatherLocation>(data);
        DetermineWeather(location);
    }

    private void DetermineWeather(WeatherLocation location)
    {
        if (location.Weather[0].Main.CompareTo("Rain") == 0)
        {
            print($"it's rainy in {location.Name}");
            SetEnvironment(FindWeather("rainy"));
            return;
        }

        if (location.Weather[0].Main.CompareTo("Snow") == 0)
        {
            print($"it's snowy in {location.Name}");
            SetEnvironment(FindWeather("snowy"));
            return;
        }

        // set the skybox to time of day if a certain weather condition isn't specified
        double localTimeMins = DateTime.UtcNow.AddSeconds(location.Timezone).TimeOfDay.TotalMinutes;

        DateTime sunriseTime = DateTimeOffset.FromUnixTimeSeconds(location.Sys.Sunrise).DateTime;
        DateTime sunsetTime = DateTimeOffset.FromUnixTimeSeconds(location.Sys.Sunset).DateTime;

        double sunriseTimeMins = sunriseTime.AddSeconds(location.Timezone).TimeOfDay.TotalMinutes;
        double sunsetTimeMins = sunsetTime.AddSeconds(location.Timezone).TimeOfDay.TotalMinutes;

        if (localTimeMins <= sunriseTimeMins - 15)
        {
            // night
            print($"it's before sunrise in {location.Name}");
            SetEnvironment(FindWeather("nighttime"));
        }
        else if (localTimeMins <= sunriseTimeMins + 15)
        {
            // sunrise
            print($"it's sunrise in {location.Name}");
            SetEnvironment(FindWeather("sunrise"));
        }
        else if (localTimeMins <= sunsetTimeMins - 15)
        {
            // day
            print($"it's daytime in {location.Name}");
            SetEnvironment(FindWeather("daytime"));
        }
        else if (localTimeMins <= sunsetTimeMins + 15)
        {
            // sunset
            print($"it's sunset in {location.Name}");
            SetEnvironment(FindWeather("sunset"));
        }
        else
        {
            // night
            print($"it's after sunset in {location.Name}");
            SetEnvironment(FindWeather("nighttime"));
        }
    }

    private WeatherSO FindWeather(string weatherName)
    {
        foreach (WeatherSO weather in weatherTypes)
        {
            if (weather.Name == weatherName)
                return weather;
        }
        return null;
    }

    private void SetEnvironment(WeatherSO weather)
    {
        if (weather == null)
        {
            Debug.LogWarning("Weather not found in list of mats");
            return;
        }

        RenderSettings.skybox = weather.skyboxMat;
        sun.color = weather.sunColor;
    }
  

}
