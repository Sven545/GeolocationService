using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeolocationServire.BusinessLogicLayer.DataTransferObjects;
using GeolocationServire.BusinessLogicLayer.Interfaces;
using System.Text.Json.Serialization;
using GeolocationServire.BusinessLogicLayer.Objects.Url;

namespace GeolocationServire.BusinessLogicLayer.Services
{
    public class DadataService : ILocationToAddress
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonSerializerOptions;
        public DadataService(HttpClient client)
        {
            _client = client;
        }
        public IEnumerable<AddressDTO> GetAddresses(LocationDTO geodata, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
