using GeolocationServire.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.Objects
{
    public class Addresses
    {
        [JsonPropertyName("data")]
        public AddressDTO Address { get; set; }

    }
}
