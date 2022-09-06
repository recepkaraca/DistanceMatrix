using Newtonsoft.Json;

namespace DistanceMatrix.Entities
{
    public class Location
    {
        [JsonProperty("areaCode")] 
        public string AreaCode { get; set; }
        [JsonProperty("corridor")] 
        public string Corridor { get; set; }
        [JsonProperty("module")] 
        public long Module { get; set; }
    }
}