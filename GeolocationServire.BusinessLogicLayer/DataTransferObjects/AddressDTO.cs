using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.DataTransferObjects
{
    public class AddressDTO
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

    }
}
