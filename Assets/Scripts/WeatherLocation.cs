using Newtonsoft.Json;
using System.Collections.Generic;

public class WeatherLocation
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("sys")]
    public WeatherLocation_Sys Sys { get; set; }

    [JsonProperty("weather")]
    public List<WeatherLocation_Weather> Weather { get; set; }
}

public class WeatherLocation_Sys
{
    [JsonProperty("sunrise")]
    public int Sunrise { get; set; }

    [JsonProperty("sunset")]
    public int Sunset { get; set; }
}

public class WeatherLocation_Weather
{
    [JsonProperty("main")]
    public string Main { get; set; }
}
