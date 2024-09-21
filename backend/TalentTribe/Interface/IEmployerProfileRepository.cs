using TalentTribe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static TalentTribe.Controllers.EmployerProfilesController;

namespace TalentTribe.Interface
{
    public interface IEmployerProfileRepository
    {
        Task<IEnumerable<EmployerProfile>> GetAllEmployerProfilesAsync();
        Task<EmployerProfile> GetEmployerProfileByIdAsync(int id);
        Task AddEmployerProfileAsync(EmployerProfile employerProfile);
        Task<bool> UpdateEmployerProfileAsync(EmployerProfile updatedProfile);
        Task<bool> DeleteEmployerProfileAsync(int id);

        Task<IEnumerable<Company>> GetCompaniesByEmployerProfileIdAsync(int employerProfileId);
        Task<IEnumerable<Application>> GetApplicationsByEmployerProfileIdAsync(int employerProfileId);
        Task<IEnumerable<Job>> GetJobsByEmployerProfileIdAsync(int employerProfileId);
        Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId);
        Task<IEnumerable<Interview>> GetInterviewsByEmployerProfileIdAsync(int employerProfileId);
        Task<JobSeekerDetailsResponse?> GetJobSeekerDetailsByApplicationIdAsync(int applicationId);
    }
}
