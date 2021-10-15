using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapAPI.Models.Repository;

namespace MapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapLocationController : ControllerBase
    {
        private IMapLocationRepository mapLocationRepository;

        public MapLocationController(IMapLocationRepository locationRepository)
        {
            this.mapLocationRepository = locationRepository;
        }

        [HttpGet]
        [Route("api/datapoints")]
        public IActionResult GetDatapoints()
        {
            try
            {
                var messages = mapLocationRepository.GetAll();
                if (messages == null)
                {
                    return NotFound();
                }

                return Ok(messages);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
