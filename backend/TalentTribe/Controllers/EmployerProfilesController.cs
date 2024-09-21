using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentTribe.Interface;
using TalentTribe.Models;
using TalentTribe.Service;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employer")]

    public class EmployerProfilesController : ControllerBase
    {
        private readonly EmployerProfileService _employerProfileService;

        public EmployerProfilesController(EmployerProfileService employerProfileService)
        {
            _employerProfileService = employerProfileService;
        }

        // GET: api/EmployerProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerProfile>>> GetEmployerProfiles()
        {
            var employerProfiles = await _employerProfileService.GetAllEmployerProfilesAsync();
            return Ok(employerProfiles);
        }

        // GET: api/EmployerProfiles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerProfile>> GetEmployerProfileById(int id)
        {
            var employerProfile = await _employerProfileService.GetEmployerProfileByIdAsync(id);
            if (employerProfile == null)
            {
                return NotFound("Employer profile not found.");
            }

            return Ok(employerProfile);
        }

        // PUT: api/EmployerProfiles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployerProfile(int id, [FromBody] EmployerProfile updatedProfile)
        {
            var result = await _employerProfileService.UpdateEmployerProfileAsync(id, updatedProfile);
            if (!result)
            {
                return NotFound("Employer profile not found.");
            }

            return Ok(updatedProfile);
        }

        // GET: api/EmployerProfiles/companyByEmployerProfile/{employerProfileId}
        [HttpGet("companyByEmployerProfile/{employerProfileId}")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesByEmployerProfileId(int employerProfileId)
        {
            var companies = await _employerProfileService.GetCompaniesByEmployerProfileIdAsync(employerProfileId);
            if (companies == null || !companies.Any())
            {
                return NotFound($"No companies found for EmployerProfileId: {employerProfileId}");
            }

            return Ok(companies);
        }

        // GET: api/EmployerProfiles/applicationsByEmployer/{employerProfileId}
        [HttpGet("applicationsByEmployer/{employerProfileId}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplicationsByEmployerProfileId(int employerProfileId)
        {
            var applications = await _employerProfileService.GetApplicationsByEmployerProfileIdAsync(employerProfileId);
            if (applications == null || !applications.Any())
            {
                return NotFound($"No applications found for EmployerProfileId: {employerProfileId}");
            }

            return Ok(applications);
        }

        // GET: api/EmployerProfiles/JobsOfEmployer/{employerProfileId}
        [HttpGet("JobsOfEmployer/{employerProfileId}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByEmployerProfileId(int employerProfileId)
        {
            var jobs = await _employerProfileService.GetJobsByEmployerProfileIdAsync(employerProfileId);
            if (jobs == null || !jobs.Any())
            {
                return NotFound("No jobs found for this employer.");
            }

            return Ok(jobs);
        }

        // GET: api/EmployerProfiles/applicationByJobId/{jobId}
        [HttpGet("applicationByJobId/{jobId}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplicationsByJobId(int jobId)
        {
            var applications = await _employerProfileService.GetApplicationsByJobIdAsync(jobId);
            if (applications == null || !applications.Any())
            {
                return NotFound("No applications found for this job.");
            }

            return Ok(applications);
        }

        // GET: api/EmployerProfiles/interviewsByEmployer/{employerProfileId}
        [HttpGet("interviewsByEmployer/{employerProfileId}")]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterviewsByEmployerProfileId(int employerProfileId)
        {
            var interviews = await _employerProfileService.GetInterviewsByEmployerProfileIdAsync(employerProfileId);
            if (interviews == null || !interviews.Any())
            {
                return NotFound($"No interviews found for EmployerProfileId: {employerProfileId}");
            }

            return Ok(interviews);
        }

        // GET: api/EmployerProfiles/jobseekerDetails/{applicationId}
        [HttpGet("jobseekerDetails/{applicationId}")]
        public async Task<ActionResult<JobSeekerDetailsResponse>> GetJobSeekerDetailsByApplicationId(int applicationId)
        {
            var jobSeekerDetails = await _employerProfileService.GetJobSeekerDetailsByApplicationIdAsync(applicationId);
            if (jobSeekerDetails == null)
            {
                return NotFound($"No job seeker found for ApplicationId: {applicationId}");
            }

            return Ok(jobSeekerDetails);
        }

        // POST: api/EmployerProfiles
        [HttpPost]
        public async Task<ActionResult<EmployerProfile>> PostEmployerProfile(EmployerProfile employerProfile)
        {
            await _employerProfileService.AddEmployerProfileAsync(employerProfile);
            return CreatedAtAction(nameof(GetEmployerProfileById), new { id = employerProfile.EmployerProfileId }, employerProfile);
        }

        // DELETE: api/EmployerProfiles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployerProfile(int id)
        {
            var result = await _employerProfileService.DeleteEmployerProfileAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
// DTO for the credentials
public class CredentialDto
{
    public int CredentialId { get; set; }
    public string? CredentialName { get; set; }
    public string? CredentialUrl { get; set; }
    public string? IssuedBy { get; set; }
    public DateTime? IssueDate { get; set; }
    public string? CredentialType { get; set; }
}

// Response object for job seeker details and credentials
public class JobSeekerDetailsResponse
{
    public int JobSeekerProfileId { get; set; }
    public string? FullName { get; set; }
    public string? Skills { get; set; }
    public string? Experience { get; set; }
    public string? Education { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? ResumeUrl { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public List<CredentialDto>? Credentials { get; set; }
}
