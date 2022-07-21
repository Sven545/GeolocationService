using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.Objects.Url
{
    public class NominatimUrl
    {
        public bool EndpointIsAdded { get; set; }
        public bool CountryIsAdded { get; set; }
        public bool CityIsAdded { get; set; }
        public bool StreetIsAdded { get; set; }
        public bool HouseIsAdded { get; set; }
        public bool FormatIsAdded { get; set; }
        public bool LimitIsAdded { get; set; }
        public StringBuilder UrlAddress { get; set; }
        public NominatimUrl()
        {
            UrlAddress = new StringBuilder();
            UrlAddress.Append("https");
            UrlAddress.Append(@"://");
            UrlAddress.Append("nominatim.openstreetmap.org");
            
        }
        public override string ToString()
        {
            return UrlAddress.ToString();
        }


    }
}
