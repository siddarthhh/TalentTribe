using TalentTribe.Interface;
using TalentTribe.Models;
using static TalentTribe.repository.CompanyRepository;

namespace TalentTribe.Service
{

    public class CompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return _companyRepository.GetAllCompaniesAsync();
        }

        public Task<Company> GetCompanyByIdAsync(int id)
        {
            return _companyRepository.GetCompanyByIdAsync(id);
        }

        public Task<CompanyDto> GetCompanyByEmployerProfileIdAsync(int employerProfileId)
        {

            return _companyRepository.GetCompanyByEmployerProfileIdAsync(employerProfileId);
        }

        public Task AddCompanyAsync(Company company)
        {
            return _companyRepository.AddCompanyAsync(company);
        }

        public Task UpdateCompanyAsync(Company company)
        {
            return _companyRepository.UpdateCompanyAsync(company);
        }

        public Task DeleteCompanyAsync(int id)
        {
            return _companyRepository.DeleteCompanyAsync(id);
        }

        public bool CompanyExists(int id)
        {
            return _companyRepository.CompanyExists(id);
        }
    }
}
