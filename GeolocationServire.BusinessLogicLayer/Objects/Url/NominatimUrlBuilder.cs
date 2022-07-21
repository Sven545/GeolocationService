using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.Objects.Url
{
    public static class NominatimUrlBuilder
    {
        public static NominatimUrl AddEndpoint(this NominatimUrl url,string endpoint)
        {
            if(!url.EndpointIsAdded)
            {
                url.UrlAddress.Append($"/{endpoint}?");
                url.EndpointIsAdded = true;
            }
            
            return url;
        }

        public static NominatimUrl AddCountry(this NominatimUrl url,string country)
        {
            if (!url.CountryIsAdded)
            {
                url.UrlAddress.Append($"country={country}&");
                url.CountryIsAdded = true;
            }
            
            return url;
        }
        public static NominatimUrl AddCity(this NominatimUrl url, string city)
        {
            if(!url.CityIsAdded&&url.CountryIsAdded)
            {
                url.UrlAddress.Append($"city={city}&");
                url.CityIsAdded = true;
            }
            
            return url;
        }
        public static NominatimUrl AddStreet(this NominatimUrl url, string street)
        {
            if(!url.StreetIsAdded&&url.CityIsAdded)
            {
                url.UrlAddress.Append($"street={street}");
                url.StreetIsAdded = true;
            }
           
            return url;
        }
        public static NominatimUrl AddHouse(this NominatimUrl url, string house)
        {
            if(!url.HouseIsAdded&&url.StreetIsAdded)
            {
                url.UrlAddress.Append($" {house}");
                url.HouseIsAdded = true;
            }
           
            return url;
        }
        public static NominatimUrl AddFormat(this NominatimUrl url, string format)
        {
            if(!url.FormatIsAdded)
            {
                url.UrlAddress.Append($"&format={format}&");
                url.FormatIsAdded = true;
            }
           
            return url;
        }
        public static NominatimUrl AddLimit(this NominatimUrl url, int limit)
        {
            if(!url.LimitIsAdded)
            {
                url.UrlAddress.Append($"limit={limit}");
                url.LimitIsAdded = true;
            }
           
            return url;
        }
    }
}
