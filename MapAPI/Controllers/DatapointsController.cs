using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapAPI.Models;

namespace MapAPI.Controllers
{
    #region DatapointController
    [Route("api/[controller]")]
    [ApiController]
    public class DatapointsController : ControllerBase
    {
        private readonly DatapointContext _context;

        public DatapointsController(DatapointContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/Datapoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Datapoint>>> GetDatapoints()
        {
            return await _context.Datapoints.ToListAsync();
        }

        // GET: api/Datapoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Datapoint>> GetDatapoint(long id)
        {
            var datapoint = await _context.Datapoints.FindAsync(id);

            if (datapoint == null)
            {
                return NotFound();
            }

            return datapoint;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Update
        // PUT: api/Datapoints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatapoint(long id, Datapoint datapoint)
        {
            if (id != datapoint.ID)
            {
                return BadRequest();
            }

            _context.Entry(datapoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatapointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_Create
        // POST: api/Datapoints
        [HttpPost]
        public async Task<ActionResult<Datapoint>> PostDatapoint(Datapoint datapoint)
        {
            _context.Datapoints.Add(datapoint);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDatapoint", new { id = datapoint.ID }, datapoint);
            return CreatedAtAction(nameof(GetDatapoint), new { id = datapoint.ID }, datapoint);
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Datapoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatapoint(long id)
        {
            var datapoint = await _context.Datapoints.FindAsync(id);
            if (datapoint == null)
            {
                return NotFound();
            }

            _context.Datapoints.Remove(datapoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool DatapointExists(long id)
        {
            return _context.Datapoints.Any(e => e.ID == id);
        }
    }
}