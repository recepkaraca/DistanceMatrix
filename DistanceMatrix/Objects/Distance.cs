using Newtonsoft.Json;

namespace DistanceMatrix.Objects
{
    public class Distance
    {
        [JsonProperty("value")] 
        public long Value { get; set; }
    }
}