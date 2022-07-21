using Microsoft.AspNetCore.Mvc;
using GeolocationServire.BusinessLogicLayer.Interfaces;
using GeolocationService.PresentationLayer.ViewModels;
using System;
using AutoMapper;
using GeolocationServire.BusinessLogicLayer.DataTransferObjects;
using System.Collections.Generic;

namespace GeolocationService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/geolocation")]
    public class GeolocationController : Controller
    {
        private ILocationToAddress _locationToAddressService;
        private IAddressToLocation _addressToLocationService;
        private Mapper _mapper;
        public GeolocationController(ILocationToAddress locationToAddressService, IAddressToLocation addressToLocationService)
        {
            _locationToAddressService = locationToAddressService;
            _addressToLocationService = addressToLocationService;

            var mapperConfig = new MapperConfiguration(config =>
         {
             config.CreateMap<LocationModel, LocationDTO>().ReverseMap();
             config.CreateMap<AddressModel, AddressDTO>().ReverseMap();
         }
            );
            _mapper = new Mapper(mapperConfig);
        }

        [HttpPost]
        [Route("reverse")]
        public IActionResult Get10Addresses(LocationModel locationModel)
        {
            try
            {
                var locationDTO = _mapper.Map<LocationModel, LocationDTO>(locationModel);
                var addressesDTO = _locationToAddressService.GetAddressesAsync(locationDTO, 10);
                var addressesModel = _mapper.Map<IEnumerable<AddressDTO>, IEnumerable<AddressModel>>(addressesDTO);
                return Ok(addressesModel);
            }
            catch (ArgumentException ex)
            {
                return NoContent();
            }
            catch (Exception)
            {
                return Problem("Something wrong");
            }

        }

        [HttpPost]
        [Route("search")]
        public IActionResult GetGeodata(AddressModel addressModel)
        {


            try
            {
                var addressDTO = _mapper.Map<AddressModel, AddressDTO>(addressModel);
                var locationDTO = _addressToLocationService.GetLocationAsync(addressDTO);
                var locationModel = _mapper.Map<LocationDTO, LocationModel>(locationDTO);
                return Ok(locationModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Problem("Something wrong");
            }
        }
    }
}
