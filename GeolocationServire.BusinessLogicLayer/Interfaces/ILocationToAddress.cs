using GeolocationServire.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.Interfaces
{
    public interface ILocationToAddress
    {
        IEnumerable<AddressDTO> GetAddresses(LocationDTO geodata,int amount);
    }
}
