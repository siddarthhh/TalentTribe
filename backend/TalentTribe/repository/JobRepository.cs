using Microsoft.EntityFrameworkCore;
using TalentTribe.Interface;
using TalentTribe.Models;

namespace TalentTribe.repository
{
    public class JobRepository : IJobRepository
    {
        private readonly TalentTribeDbContext _context;

        public JobRepository(TalentTribeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Company> GetCompanyByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.EmployerProfileId == employerProfileId);
        }

        public async Task AddJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job updatedJob)
        {
            var existingJob = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == updatedJob.JobId);
            if (existingJob != null)
            {
                existingJob.JobTitle = updatedJob.JobTitle ?? existingJob.JobTitle;
                existingJob.companyName = updatedJob.companyName ?? existingJob.companyName;
                existingJob.JobDescription = updatedJob.JobDescription ?? existingJob.JobDescription;
                existingJob.EmploymentType = updatedJob.EmploymentType ?? existingJob.EmploymentType;
                existingJob.SalaryRange = updatedJob.SalaryRange ?? existingJob.SalaryRange;
                existingJob.Location = updatedJob.Location ?? existingJob.Location;
                existingJob.JobStatus = updatedJob.JobStatus ?? existingJob.JobStatus;
                existingJob.ApplicationDeadline = updatedJob.ApplicationDeadline ?? existingJob.ApplicationDeadline;
                existingJob.ExperienceLevel = updatedJob.ExperienceLevel ?? existingJob.ExperienceLevel;
                existingJob.RequiredSkills = updatedJob.RequiredSkills ?? existingJob.RequiredSkills;
                existingJob.IsActive = updatedJob.IsActive;

                _context.Jobs.Update(existingJob);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}