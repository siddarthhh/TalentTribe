using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly TalentTribeDbContext _context;

        public AdminsController(TalentTribeDbContext context)
        {
            _context = context;
        }

        // New action to get employers with the most jobs
        [HttpGet("EmployersWithMostJobs")]
        public async Task<IActionResult> GetEmployersWithMostJobs()
        {
            var employerJobCounts = await _context.Jobs
                .Where(j => j.IsActive)
                .GroupBy(j => new { j.EmployerProfileId, j.EmployerProfile.FullName })
                .Select(group => new
                {
                    EmployerProfileId = group.Key.EmployerProfileId,
                    EmployerName = group.Key.FullName,
                    JobCount = group.Count()
                })
                .OrderByDescending(result => result.JobCount)
                .ToListAsync();

            return Ok(employerJobCounts);
        }


        // GET: api/Job/CompaniesWithMostJobs
        [HttpGet("CompaniesWithMostJobs")]
        public async Task<IActionResult> GetCompaniesWithMostJobs()
        {
            var companyJobCounts = await _context.Jobs
                .Where(j => j.IsActive)
                .GroupBy(j => j.companyName)
                .Select(group => new
                {
                    CompanyName = group.Key,
                    JobCount = group.Count()
                })
                .OrderByDescending(result => result.JobCount)
                .ToListAsync();

            return Ok(companyJobCounts);
        }


        // Get all jobs asynchronously  
        [HttpGet("job")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.Jobs
                .Include(j => j.EmployerProfile)
                .ToListAsync();

            return Ok(jobs);
        }

        // Delete job by id asynchronously
        [HttpDelete("job/{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job deleted successfully" });
        }
        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
