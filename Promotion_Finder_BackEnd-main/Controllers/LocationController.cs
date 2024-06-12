using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubwayPromotion.Models;
using SubwayPromotion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubwayPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;
        private readonly IMapper mapper;

        public LocationController(LocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            this.mapper = mapper;
        }

        // GET: api/Location/countries
        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<string>>> GetCountries()
        {
            return await _locationService.GetAllCountries();
        }
        // Get: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            return (await _locationService.GetAll()).ToList();
        }
        //POST: api/Location
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(Location location)
        {
            var locationDto = mapper.Map<LocationDTO>(location);
            await _locationService.Add(locationDto);
            return Created("", location);
        }
    }
}