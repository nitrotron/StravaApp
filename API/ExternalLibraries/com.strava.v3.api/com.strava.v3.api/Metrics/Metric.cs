using Newtonsoft.Json;

namespace com.strava.v3.api.Metrics
{
    public class Metric
    {
        [JsonProperty("distance")]
        public float Distance { get; set; }

        [JsonProperty("elapsed_time")]
        public int ElapsedTime { get; set; }

        [JsonProperty("elevation_difference")]
        public float? ElevationDifference { get; set; }

        [JsonProperty("moving_time")]
        public int MovingTime { get; set; }

        [JsonProperty("split")]
        public int Split { get; set; }

        [JsonProperty("average_speed")]
        public float AverageSpeed { get; set; }

        [JsonProperty("pace_zone")]
        public int PaceZone { get; set; }
    }
}