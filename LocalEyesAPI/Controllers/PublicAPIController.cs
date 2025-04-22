using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalEyesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicAPIController : Controller
    {

        private readonly LocalEyesDbContext _context;

        public PublicAPIController(LocalEyesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all reports | JWT Auth
        /// </summary>
        [HttpGet("ReportsExternal")]
        [ServiceFilter<JwtAuthFilter>]
        public async Task<IActionResult> ReportsExternal()
        {
            var reports = await _context.Reports.ToListAsync();


            return Ok(reports);
        }
    }
}
