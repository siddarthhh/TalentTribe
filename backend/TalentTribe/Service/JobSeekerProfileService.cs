using TalentTribe.Models;

namespace TalentTribe.Service
{
    public class JobSeekerProfileService
    {
        private readonly IJobSeekerProfileRepository _repository;

        public JobSeekerProfileService(IJobSeekerProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobSeekerProfile>> GetJobSeekerProfilesAsync()
        {
            return await _repository.GetJobSeekerProfilesAsync();
        }

        public async Task<JobSeekerProfile?> GetJobSeekerProfileByIdAsync(int id)
        {
            return await _repository.GetJobSeekerProfileByIdAsync(id);
        }

        public async Task<string?> DownloadResumeAsync(int id)
        {
            return await _repository.DownloadResumeAsync(id);
        }

        public async Task<string?> DownloadProfilePictureAsync(int id)
        {
            return await _repository.DownloadProfilePictureAsync(id);
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _repository.GetApplicationsByJobSeekerProfileIdAsync(jobSeekerProfileId);
        }

        public async Task<IEnumerable<Interview>> GetInterviewsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _repository.GetInterviewsByJobSeekerProfileIdAsync(jobSeekerProfileId);
        }

        public async Task<IEnumerable<Credential>> GetCredentialsByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _repository.GetCredentialsByJobSeekerProfileIdAsync(jobSeekerProfileId);
        }

        public async Task UpdateJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            await _repository.UpdateJobSeekerProfileAsync(profile);
        }

        public async Task AddJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            await _repository.AddJobSeekerProfileAsync(profile);
        }

        public async Task DeleteJobSeekerProfileAsync(JobSeekerProfile profile)
        {
            await _repository.DeleteJobSeekerProfileAsync(profile);
        }
    }

}
