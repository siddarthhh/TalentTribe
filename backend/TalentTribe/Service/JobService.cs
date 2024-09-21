using TalentTribe.Interface;
using TalentTribe.Models;

namespace TalentTribe.Service
{

    public class JobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return _jobRepository.GetAllJobsAsync();
        }

        public Task<Job> GetJobByIdAsync(int id)
        {
            return _jobRepository.GetJobByIdAsync(id);
        }

        public Task<Company> GetCompanyByEmployerProfileIdAsync(int employerProfileId)
        {
            return _jobRepository.GetCompanyByEmployerProfileIdAsync(employerProfileId);
        }

        public Task AddJobAsync(Job job)
        {
            return _jobRepository.AddJobAsync(job);
        }

        public Task UpdateJobAsync(Job job)
        {
            return _jobRepository.UpdateJobAsync(job);
        }

        public Task DeleteJobAsync(int id)
        {
            return _jobRepository.DeleteJobAsync(id);
        }

        public bool JobExists(int id)
        {
            return _jobRepository.JobExists(id);
        }
    }
}
