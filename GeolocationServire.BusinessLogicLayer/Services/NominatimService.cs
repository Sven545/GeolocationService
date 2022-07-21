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
    public class NominatimService : IAddressToLocation
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonSerializerOptions;
        public NominatimService(HttpClient client)
        {
            _client = client;
            client.DefaultRequestHeaders.Add("user-agent", "GeolocationService");

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
        }
        public LocationDTO GetLocationAsync(AddressDTO address)
        {
            try
            {
                return GetLocation(address).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ArgumentException)
                {

                    throw ex.InnerException;
                }
                else throw;
            }

            catch (Exception)
            {
                throw;
            }

        }
        private async Task<LocationDTO> GetLocation(AddressDTO address)
        {
            NominatimUrl url = new NominatimUrl()
                .AddEndpoint("search")
                .AddCountry(address.Country)
                .AddCity(address.City)
                .AddStreet(address.Street)
                .AddHouse(address.House)
                .AddFormat("json")
                .AddLimit(1);
            var test = url.ToString();

            var result = await _client.GetStringAsync(url.ToString());
            List<LocationDTO> locations = JsonSerializer.Deserialize<List<LocationDTO>>(result, _jsonSerializerOptions);

            var location = locations.FirstOrDefault() ?? throw new ArgumentException("No data for this address");
            return location;


        }

    }
}
