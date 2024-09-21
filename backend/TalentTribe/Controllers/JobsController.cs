using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;
using TalentTribe.Service;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobService _jobService;

        public JobsController(JobService jobService)
        {
            _jobService = jobService;
        }

        // GET: api/Jobs
        [HttpGet]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();
            return Ok(jobs);
        }





        // GET: api/Companies/ByEmployerProfileId/5
        [HttpGet("companyByEmployerProfileId/{employerProfileId}")]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<ActionResult<Company>> GetCompanyByEmployerProfileId(int employerProfileId)
        {
            var company = await _jobService.GetCompanyByEmployerProfileIdAsync(employerProfileId);
            if (company == null)
            {
                return NotFound(new { message = "Company not found for the given EmployerProfileId" });
            }

            return Ok(company);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/Jobs/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job updatedJob)
        {
            if (!_jobService.JobExists(id))
            {
                return NotFound(new { message = "Job not found" });
            }

            await _jobService.UpdateJobAsync(updatedJob);
            return Ok(updatedJob);
        }

        // POST: api/Jobs
        [HttpPost]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            await _jobService.AddJobAsync(job);
            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteJob(int id)
        {
            if (!_jobService.JobExists(id))
            {
                return NotFound();
            }

            await _jobService.DeleteJobAsync(id);
            return NoContent();
        }
    }
}