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
using System.Net.Http.Json;
using System.Net.Http.Headers;
using GeolocationServire.BusinessLogicLayer.Objects;


namespace GeolocationServire.BusinessLogicLayer.Services
{
    public class DadataService : ILocationToAddress
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonSerializerOptions;
        public DadataService(HttpClient client)
        {
            _client = client;
            

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
        }

        public IEnumerable<AddressDTO> GetAddressesAsync(LocationDTO geodata, int amount)
        {
            try
            {
                return GetAddresses(geodata, amount).Result;
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

        public async Task<IEnumerable<AddressDTO>> GetAddresses(LocationDTO geodata, int amount)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("user-agent", "GeolocationService");
            _client.DefaultRequestHeaders.Add("Authorization", "Token 5a3dffd6a9e892cf7fc9ec9ae709f041244bb504");

            SerializeLocation requestData=new SerializeLocation() { Latitude=geodata.Latitude,Longitude=geodata.Longitude,Amount=amount};
            JsonContent content = JsonContent.Create(requestData, typeof(SerializeLocation), null, _jsonSerializerOptions);
           

            var result = await _client.PostAsync("https://suggestions.dadata.ru/suggestions/api/4_1/rs/geolocate/address", content);           // response.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            AddressesResponse addressesResponse = JsonSerializer.Deserialize<AddressesResponse>(responseBody, _jsonSerializerOptions);
            var addresses = (from addres in addressesResponse.Addresses
                            select addres.Address).ToList();
            if(addresses.Count==0)
            {
                throw new ArgumentException("No addresses nearby");
            }

            return addresses;
            
           
        }

       
    }
}
