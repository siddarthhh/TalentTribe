using TalentTribe.Models;

namespace TalentTribe.Interface
{

    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(int id);
        Task<Company> GetCompanyByEmployerProfileIdAsync(int employerProfileId);
        Task AddJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(int id);
        bool JobExists(int id);
    }
}
