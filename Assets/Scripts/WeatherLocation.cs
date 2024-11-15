using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class WeatherLocation
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("sys")]
    public WeatherSys Sys { get; set; }
}

public class WeatherSys
{
    [JsonProperty("sunrise")]
    public int Sunrise { get; set; }

    [JsonProperty("sunset")]
    public int Sunset { get; set; }
}
