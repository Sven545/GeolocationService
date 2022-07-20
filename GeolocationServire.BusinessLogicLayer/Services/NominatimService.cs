using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeolocationServire.BusinessLogicLayer.DataTransferObjects;
using GeolocationServire.BusinessLogicLayer.Interfaces;

namespace GeolocationServire.BusinessLogicLayer.Services
{
    public class NominatimService : IAddressToLocation
    {
        public LocationDTO GetLocation(AddressDTO address)
        {
            throw new NotImplementedException();
        }
    }
}
