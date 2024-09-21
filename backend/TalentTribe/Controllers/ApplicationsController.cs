using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;
using Microsoft.AspNetCore.Authorization;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly TalentTribeDbContext _context;

        public ApplicationsController(TalentTribeDbContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }


        // Get all applications asynchronously
        [HttpGet("manage")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _context.Applications
                .Include(a => a.JobSeekerProfile)
                .Include(a => a.Job)
                .ToListAsync();

            return Ok(applications);
        }

        // Delete application asynchronously
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Application deleted successfully" });
        }
        

        // GET: api/applications/{id}
        [HttpGet("Apply/{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _context.Applications
                .Include(a => a.JobSeekerProfile)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }
        // GET: api/Applications/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<Application>> GetApplication1(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }
       

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<IActionResult> UpdateApplication(int id, [FromBody] ApplicationUpdatePayload updatePayload)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            // Update only the provided fields
            application.Status = updatePayload.Status;
            application.Feedback = updatePayload.Feedback;
           

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        // POST: api/Applications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            // Check if the job seeker has already applied for this job
            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobId == application.JobId && a.JobSeekerProfileId == application.JobSeekerProfileId);

            if (existingApplication != null)
            {
                // Return a message if the job seeker has already applied
                return BadRequest(new { message = "You have already applied for this job." });
            }

            // Proceed to add the application if no existing one was found
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.ApplicationId }, application);
        }


       
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
        // GET: api/Interviews/byApplication/{applicationId}
        [HttpGet("InterviewsbyApplication/{applicationId}")]
        public async Task<ActionResult<IEnumerable<InterviewDto>>> GetInterviewsByApplicationId(int applicationId)
        {
            // Step 1: Retrieve the interviews for the given ApplicationId
            var interviews = await _context.Interviews
                .Where(i => i.ApplicationId == applicationId)
                .ToListAsync();

            // Step 2: Check if interviews exist for the given application
            if (interviews == null || !interviews.Any())
            {
                return NotFound($"No interviews found for ApplicationId: {applicationId}");
            }

            // Step 3: Map interviews to DTOs
            var interviewDtos = interviews.Select(i => new InterviewDto
            {
                InterviewId = i.InterviewId,
                InterviewDate = i.InterviewDate,
                InterviewType = i.InterviewType,
                InterviewLink = i.InterviewLink,
                InterviewLocation = i.InterviewLocation,
                InterviewFeedback = i.InterviewFeedback
            }).ToList();

            return Ok(interviewDtos);
        }


    }
    public class ApplicationUpdatePayload
    {
        public string? Status { get; set; }
        public string? Feedback { get; set; }

    }
    // DTO for the interviews
    public class InterviewDto
    {
        public int InterviewId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? InterviewType { get; set; }
        public string? InterviewLink { get; set; }
        public string? InterviewLocation { get; set; }
        public string? InterviewFeedback { get; set; }

    
}


}
