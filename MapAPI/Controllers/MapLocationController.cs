using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapAPI.Models.Repository;
using MapAPI.Models;

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

        [HttpGet("{id}")]
        public IActionResult GetSingleDatapoint(int id)
        {
            try
            {
                var message = mapLocationRepository.Get(id);
                if (message == null) {
                    return NotFound();
                }

                return Ok(message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("add")]
        public string PostDatapoint(MapLocationDatum datapointToEnter)
        {
            try
            {
                mapLocationRepository.Add(datapointToEnter);
                return "Successfully added to repository";
            }
            catch (Exception)
            {
                return "An error occurred while posting";
            }
        }
        [HttpDelete]
        [Route("remove")]
        public string DeleteDatapoint(int id)
        {
            try
            {
                mapLocationRepository.Delete(id);
                return "Successfully removed from repository";
            }
            catch (Exception)
            {
                return "An error occurred while deleting";
            }
        }
    }
}
