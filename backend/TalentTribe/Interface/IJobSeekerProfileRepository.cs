using TalentTribe.Models;

public interface IJobSeekerProfileRepository
{
    Task<IEnumerable<JobSeekerProfile>> GetJobSeekerProfilesAsync();
    Task<JobSeekerProfile?> GetJobSeekerProfileByIdAsync(int id);
    Task<bool> JobSeekerProfileExistsAsync(int id);
    Task<string?> DownloadResumeAsync(int id);
    Task<string?> DownloadProfilePictureAsync(int id);
    Task<IEnumerable<Application>> GetApplicationsByJobSeekerProfileIdAsync(int jobSeekerProfileId);
    Task<IEnumerable<Interview>> GetInterviewsByJobSeekerProfileIdAsync(int jobSeekerProfileId);
    Task<IEnumerable<Credential>> GetCredentialsByJobSeekerProfileIdAsync(int jobSeekerProfileId);
    Task UpdateJobSeekerProfileAsync(JobSeekerProfile profile);
    Task AddJobSeekerProfileAsync(JobSeekerProfile profile);
    Task DeleteJobSeekerProfileAsync(JobSeekerProfile profile);
}
