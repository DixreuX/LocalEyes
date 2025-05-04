using LocalEyes.Shared.Models;
using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalEyesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunicipalityController : Controller
    {

        private readonly LocalEyesDbContext _context;

        public MunicipalityController(LocalEyesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all municipalities | Basic Auth
        /// </summary>
        [HttpGet("Municipalities")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> GetMunicipalities()
        {
            var municipalities = await _context.Municipalities.ToListAsync();
            return Ok(municipalities);
        }

        /// <summary>
        /// Get a municipality by ID | Basic Auth
        /// </summary>
        [HttpGet("{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> GetMunicipalityById(Guid id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
            {
                return NotFound($"Municipality with ID {id} not found.");
            }

            return Ok(municipality);
        }

        /// <summary>
        /// Create a new municipality | Basic Auth
        /// </summary>
        [HttpPost("Create")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> CreateMunicipality([FromBody] Municipality municipality)
        {
            if (municipality == null)
            {
                return BadRequest("Municipality cannot be null.");
            }

            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return Ok(municipality);
        }

        /// <summary>
        /// Update an existing municipality | Basic Auth
        /// </summary>
        [HttpPut("Update/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> UpdateMunicipality(Guid id, [FromBody] Municipality municipality)
        {
            var existingMunicipality = await _context.Municipalities.FindAsync(id);

            if (existingMunicipality == null)
            {
                return NotFound($"Municipality with ID {id} not found.");
            }

            existingMunicipality.Name = municipality.Name;
            existingMunicipality.Zipcode = municipality.Zipcode;

            await _context.SaveChangesAsync();

            return Ok(existingMunicipality);
        }

        /// <summary>
        /// Delete a municipality by ID | Basic Auth
        /// </summary>
        [HttpDelete("Delete/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> DeleteMunicipality(Guid id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
            {
                return NotFound($"Municipality with ID {id} not found.");
            }

            _context.Municipalities.Remove(municipality);
            await _context.SaveChangesAsync();

            return Ok($"Municipality with ID {id} has been deleted.");
        }
    }
}

