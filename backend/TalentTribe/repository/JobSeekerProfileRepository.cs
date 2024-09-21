using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;

namespace TalentTribe.repository
{
    public class JobSeekerProfileRepository : IJobSeekerProfileRepository
    {
        private readonly TalentTribeDbContext _context;

        public JobSeekerProfileRepository(TalentTribeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobSeekerProfile>> GetJobSeekerProfilesAsync()
        {
            return await _context.JobSeekerProfiles.ToListAsync();
        }

        public async Task<JobSeekerProfile?> GetJobSeekerProfileByIdAsync(int id)
        {
            return await _context.JobSeekerProfiles.FindAsync(id);
        }

        public async Task<bool> JobSeekerProfileExistsAsync(int id)
        {
            return await _context.JobSeekerProfiles.AnyAsync(e => e.JobSeekerProfileId == id);
        }

        public async Task<string?> DownloadResumeAsync(int id)
        {
            var profile = await _context.JobSeekerProfiles.FindAsync(id);
            return profile?.ResumeUrl;
        }

        public async Task<string?> DownloadProfilePictureAsync(int id)
        {
            var profile = await _context.JobSeekerProfiles.FindAsync(id);
            return profile?.ProfilePictureUrl;
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _context.Applications
                .Where(a => a.JobSeekerProfileId == jobSeekerProfileId)
                .Include(a => a.Job)
                .Include(a => a.Interviews)
                .ToListAsync();
        }

        public async Task<IEnumerable<Interview>> GetInterviewsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _context.Interviews
                .Include(i => i.Application)
                .Where(i => i.Application.JobSeekerProfileId == jobSeekerProfileId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Credential>> GetCredentialsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _context.Credentials
                .Where(c => c.JobSeekerProfileId == jobSeekerProfileId)
                .ToListAsync();
        }

        public async Task UpdateJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            _context.JobSeekerProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task AddJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            _context.JobSeekerProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            _context.JobSeekerProfiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }

}
