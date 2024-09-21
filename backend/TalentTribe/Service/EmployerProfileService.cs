using System.Collections.Generic;
using System.Threading.Tasks;
using TalentTribe.Interface;
using TalentTribe.Models;
using static TalentTribe.Controllers.EmployerProfilesController;

namespace TalentTribe.Service
{
    public class EmployerProfileService : IEmployerProfileService
    {
        private readonly IEmployerProfileRepository _employerProfileRepository;

        public EmployerProfileService(IEmployerProfileRepository employerProfileRepository)
        {
            _employerProfileRepository = employerProfileRepository;
        }

        public async Task<IEnumerable<EmployerProfile>> GetAllEmployerProfilesAsync()
        {
            return await _employerProfileRepository.GetAllEmployerProfilesAsync();
        }

        public async Task<EmployerProfile> GetEmployerProfileByIdAsync(int id)
        {
            return await _employerProfileRepository.GetEmployerProfileByIdAsync(id);
        }

        public async Task AddEmployerProfileAsync(EmployerProfile employerProfile)
        {
            await _employerProfileRepository.AddEmployerProfileAsync(employerProfile);
        }

        public async Task<bool> UpdateEmployerProfileAsync(int id, EmployerProfile updatedProfile)
        {
            updatedProfile.EmployerProfileId = id; // Ensure ID consistency
            return await _employerProfileRepository.UpdateEmployerProfileAsync(updatedProfile);
        }

        public async Task<bool> DeleteEmployerProfileAsync(int id)
        {
            return await _employerProfileRepository.DeleteEmployerProfileAsync(id);
        }

        public async Task<IEnumerable<Company>> GetCompaniesByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _employerProfileRepository.GetCompaniesByEmployerProfileIdAsync(employerProfileId);
        }

        public async Task<IEnumerable<Application>> GetApplicationsByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _employerProfileRepository.GetApplicationsByEmployerProfileIdAsync(employerProfileId);
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _employerProfileRepository.GetJobsByEmployerProfileIdAsync(employerProfileId);
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId)
        {
            return await _employerProfileRepository.GetApplicationsByJobIdAsync(jobId);
        }

        public async Task<IEnumerable<Interview>> GetInterviewsByEmployerProfileIdAsync(int employerProfileId)
        {
            return await _employerProfileRepository.GetInterviewsByEmployerProfileIdAsync(employerProfileId);
        }

        public async Task<JobSeekerDetailsResponse?> GetJobSeekerDetailsByApplicationIdAsync(int applicationId)
        {
            return await _employerProfileRepository.GetJobSeekerDetailsByApplicationIdAsync(applicationId);
        }
    }
}
