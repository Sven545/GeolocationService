using Microsoft.AspNetCore.Mvc;
using GeolocationServire.BusinessLogicLayer.Interfaces;
using GeolocationService.PresentationLayer.ViewModels;
using System;
using AutoMapper;
using GeolocationServire.BusinessLogicLayer.DataTransferObjects;

namespace GeolocationService.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/geolocation")]
    public class GeolocationController : Controller
    {
        private ILocationToAddress _locationToAddressService;
        private IAddressToLocation _addressToLocationService;
        private Mapper _mapper;
        public GeolocationController(/*ILocationToAddress locationToAddressService,*/ IAddressToLocation addressToLocationService)
        {
            //_locationToAddressService = locationToAddressService;
            _addressToLocationService = addressToLocationService;

            var mapperConfig = new MapperConfiguration(config =>
         {
             config.CreateMap<LocationModel, LocationDTO>().ReverseMap();
             config.CreateMap<AddressModel, AddressDTO>().ReverseMap();
         }
            );
            _mapper = new Mapper(mapperConfig);
        }
        /*
        [HttpGet]
        public IActionResult GetAddress(LocationModel geodataModel)
        {
            return View();
        }
        */
        [HttpPost]
        public IActionResult GetGeodata(AddressModel addressModel)
        {
           

            try
            {
                var addressDTO = _mapper.Map<AddressModel, AddressDTO>(addressModel);
            var locationDTO = _addressToLocationService.GetLocationAsync(addressDTO);
            var locationModel = _mapper.Map<LocationDTO, LocationModel>(locationDTO);
            return Ok(locationModel);
            }
            catch(ArgumentException ex)
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
