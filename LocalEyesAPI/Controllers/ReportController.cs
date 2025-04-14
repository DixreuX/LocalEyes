using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalEyesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {

        private readonly LocalEyesDbContext _context;
        public ReportController(LocalEyesDbContext context)
        {
            _context = context;
        }

        [HttpGet("Reports")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> Reports()
        {
            var reports = await _context.Reports.ToListAsync();


            return Ok(reports);
        }

        [HttpGet("ReportsExternal")]
        [ServiceFilter<JwtAuthFilter>]
        public async Task<IActionResult> ReportsExternal()
        {
            var reports = await _context.Reports.ToListAsync();


            return Ok(reports);
        }
    }
}
