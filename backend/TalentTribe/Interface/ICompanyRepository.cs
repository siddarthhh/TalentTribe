using TalentTribe.Models;
using static TalentTribe.repository.CompanyRepository;

namespace TalentTribe.Interface
{

    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<CompanyDto> GetCompanyByEmployerProfileIdAsync(int employerProfileId);
        Task AddCompanyAsync(Company company);
        Task UpdateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
        bool CompanyExists(int id);
    }
}
