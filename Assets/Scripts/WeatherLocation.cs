using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class WeatherLocation
{
    [JsonProperty("timezone")]
    public int Timezone { get; set; }
}
