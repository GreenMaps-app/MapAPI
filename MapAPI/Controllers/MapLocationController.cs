using Microsoft.AspNetCore.Mvc;
using System;
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

        /**
         * GET method to get all datapoints
         * Endpoint is [base URL]/
         */
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

        /**
         * GET method to get a single datapoint by ID
         * Endpoint is [base URL]/[id]
         */
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

        /**
         * POST method to add a datapoint by passing in all arguments of required of the model
         * ID doesn't have to be entered since the database will autoincrement and autocreate the ID
         * If the ID is given, it ignores it and just overrides it with the autocreated ID
         * Endpoint is [base URL]/add
         */
        [HttpPost]
        [Route("add")]
        public string PostDatapoint(MapLocationDatum datapointToEnter)
        {
            try
            {
                mapLocationRepository.Add(datapointToEnter);
                return "Successfully added to database";
            }
            catch (Exception)
            {
                return "An error occurred while posting";
            }
        }

        /**
         * PUT method to change a datapoint status by ID
         * Endpoint is [base URL]/update
         */
        [HttpPut]
        [Route("update")]
        public string UpdateDatapointStatus(int id, bool status)
        {
            try
            {
                mapLocationRepository.UpdateStatus(id, status);
                return "Successfully updated in database";
            }
            catch (Exception)
            {
                return "An error occurred while putting";
            }
        }

        /**
         * DELETE method to remove a datapoint by ID
         * Endpoint is [base URL]/remove
         */
        [HttpDelete]
        [Route("remove")]
        public string DeleteDatapoint(int id)
        {
            try
            {
                mapLocationRepository.Delete(id);
                return "Successfully removed from database";
            }
            catch (Exception)
            {
                return "An error occurred while deleting";
            }
        }
    }
}
