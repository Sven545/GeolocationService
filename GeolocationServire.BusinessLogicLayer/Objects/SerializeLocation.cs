using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.Objects
{
    public class SerializeLocation
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("count")]
        public int Amount { get; set; }
    }
}
