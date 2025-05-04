using LocalEyesAPI.Data;
using LocalEyesAPI.Filters;
using LocalEyes.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        /// <summary>
        /// Get types | Basic Auth
        /// </summary>
        [HttpGet("Types")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _context.Types
            .Select(t => new { t.Id, t.Name })
            .ToListAsync();

            return Ok(types);
        }

        /// <summary>
        /// Create a report | Basic Auth
        /// </summary>
        [HttpPost("Create")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> CreateReportAsync([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest("Report cannot be null.");
            }

            if (report.Type == null)
            {
                Debug.WriteLine("Type is null. Proceeding without it.");
            }

            _context.Reports.Add(report);

            await _context.SaveChangesAsync();

            return Ok(report);
        }

        /// <summary>
        /// Update a report using ID | Basic Auth
        /// </summary>
        [HttpPut("Update/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> UpdateReportAsync(Guid id, [FromBody] Report report)
        {
            var existingReport = await _context.Reports.FindAsync(id);

            if (existingReport == null)
            {
                return NotFound();
            }

            existingReport.Comment = report.Comment;
            existingReport.Latitude = report.Latitude;
            existingReport.Longitude = report.Longitude;
            existingReport.Priority = report.Priority;
            existingReport.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(existingReport);
        }

        /// <summary>
        /// Get report by id | Basic Auth
        /// </summary>
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

        /// <summary>
        /// Delete report using ID | Basic Auth
        /// </summary>
        [HttpDelete("Delete/{id:guid}")]
        [ServiceFilter<BasicAuthFilter>]
        public async Task<IActionResult> DeleteReportAsync(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound($"Report with ID {id} not found.");
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok($"Report with ID {id} has been deleted.");
        }
    }
}
