using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationServire.BusinessLogicLayer.DataTransferObjects
{
    public class AddressDTO
    {
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int Apartment { get; set; }

    }
}
