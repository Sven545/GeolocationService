using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GeolocationServire.BusinessLogicLayer.DataTransferObjects;

namespace GeolocationServire.BusinessLogicLayer.Objects
{
    public class AddressesResponse
    {
        [JsonPropertyName("suggestions")]
        public Addresses[] Addresses { get; set; }
    }
}
