using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using LocalEyesAPI.Models;
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

        /// <summary>
        /// Get all reports | Basic Auth
        /// </summary>
        [HttpGet("Reports")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> Reports()
        {
            var reports = await _context.Reports
           .Include(r => r.Type)
           .ToListAsync();

            return Ok(reports);
        }

        [HttpGet("Types")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _context.Types
        .Select(t => new { t.Id, t.Name }) 
        .ToListAsync();

            return Ok(types);
        }

        [HttpPost("Create")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            report.Id = Guid.NewGuid();
            report.CreatedDate = DateTime.UtcNow;
            report.ModifiedDate = DateTime.UtcNow;

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return Ok(report);
        }

        [HttpPut("Update/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> UpdateReport(Guid id, [FromBody] Report report)
        {
            var existingReport = await _context.Reports.FindAsync(id);

            if (existingReport == null)
            {
                return NotFound();
            }

            existingReport.Comment = report.Comment;
            existingReport.Latitude = report.Latitude;
            existingReport.Longtitude = report.Longtitude;
            existingReport.Priority = report.Priority;
            existingReport.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(existingReport);
        }

        [HttpGet("{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> GetReportById(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }
    }
}
