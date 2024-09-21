using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentTribe.Interface;
using TalentTribe.Models;
using static TalentTribe.Controllers.EmployerProfilesController;

namespace TalentTribe.Repository
{
    public class EmployerProfileRepository : IEmployerProfileRepository
    {
        private readonly TalentTribeDbContext _context;

        public EmployerProfileRepository(TalentTribeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployerProfile>> GetAllEmployerProfilesAsync()
        {
            return await _context.EmployerProfiles.ToListAsync();
        }

        public async Task<EmployerProfile> GetEmployerProfileByIdAsync(int id)
        {
            return await _context.EmployerProfiles
                .Include(e => e.Company)  // Include related data
                .FirstOrDefaultAsync(e => e.EmployerProfileId == id);
        }

        public async Task AddEmployerProfileAsync(EmployerProfile employerProfile)
        {
            await _context.EmployerProfiles.AddAsync(employerProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmployerProfileAsync(EmployerProfile updatedProfile)
        {
            var existingProfile = await _context.EmployerProfiles.FindAsync(updatedProfile.EmployerProfileId);

            if (existingProfile == null)
            {
                return false;
            }

            _context.Entry(existingProfile).CurrentValues.SetValues(updatedProfile);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployerProfileAsync(int id)
        {
            var employerProfile = await _context.EmployerProfiles.FindAsync(id);
            if (employerProfile == null)
            {
                return false;
            }

            _context.EmployerProfiles.Remove(employerProfile);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Company>> GetCompaniesByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _context.Companies
                .Where(c => c.EmployerProfileId == employerProfileId)
                .Include(c => c.EmployerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsByEmployerProfileIdAsync(int employerProfileId)
        {
            var jobs = await _context.Jobs
                .Where(j => j.EmployerProfileId == employerProfileId)
                .Select(j => j.JobId)
                .ToListAsync();

            return await _context.Applications
                .Where(a => jobs.Contains(a.JobId))
                .Include(a => a.Job)
                .Include(a => a.JobSeekerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _context.Jobs
                .Where(j => j.EmployerProfileId == employerProfileId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId)
        {
            return await _context.Applications
                .Where(a => a.JobId == jobId)
                .Include(a => a.JobSeekerProfile)
                .Include(a => a.Job)
                .ToListAsync();
        }

        public async Task<IEnumerable<Interview>> GetInterviewsByEmployerProfileIdAsync(int employerProfileId)
        {
            var jobs = await _context.Jobs
                .Where(j => j.EmployerProfileId == employerProfileId)
                .Select(j => j.JobId)
                .ToListAsync();

            var applications = await _context.Applications
                .Where(a => jobs.Contains(a.JobId))
                .Select(a => a.ApplicationId)
                .ToListAsync();

            return await _context.Interviews
                .Where(i => applications.Contains(i.ApplicationId))
                .Include(i => i.Application)
                .ThenInclude(a => a.Job)
                .ToListAsync();
        }

        public async Task<JobSeekerDetailsResponse?> GetJobSeekerDetailsByApplicationIdAsync(int applicationId)
        {
            var application = await _context.Applications
                .Include(a => a.JobSeekerProfile)
                .ThenInclude(jsp => jsp.Credentials)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || application.JobSeekerProfile == null)
            {
                return null;
            }

            var jobSeekerProfile = application.JobSeekerProfile;
            var credentials = jobSeekerProfile.Credentials?.Select(c => new CredentialDto
            {
                CredentialId = c.CredentialId,
                CredentialName = c.CredentialName,
                CredentialUrl = c.CredentialUrl,
                IssuedBy = c.IssuedBy,
                IssueDate = c.IssueDate,
                CredentialType = c.CredentialType
            }).ToList();

            return new JobSeekerDetailsResponse
            {
                JobSeekerProfileId = jobSeekerProfile.JobSeekerProfileId,
                FullName = jobSeekerProfile.FullName,
                Skills = jobSeekerProfile.Skills,
                Experience = jobSeekerProfile.Experience,
                Education = jobSeekerProfile.Education,
                Address = jobSeekerProfile.Address,
                City = jobSeekerProfile.City,
                State = jobSeekerProfile.State,
                Country = jobSeekerProfile.Country,
                PostalCode = jobSeekerProfile.PostalCode,
                ResumeUrl = jobSeekerProfile.ResumeUrl,
                ProfilePictureUrl = jobSeekerProfile.ProfilePictureUrl,
                Credentials = credentials
            };
        }
    }
}
