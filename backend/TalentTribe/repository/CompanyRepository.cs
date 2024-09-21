using Microsoft.EntityFrameworkCore;
using TalentTribe.Interface;
using TalentTribe.Models;

namespace TalentTribe.repository
{

    public class CompanyRepository : ICompanyRepository
    {
        private readonly TalentTribeDbContext _context;

        public CompanyRepository(TalentTribeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }
        public class CompanyDto
        {
            public int EmployerProfileId { get; set; }
            public string? CompanyName { get; set; }
            public string? CompanyDescription { get; set; }
            public string? Industry { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? WebsiteUrl { get; set; }
            public string? ContactEmail { get; set; }
            public string? ContactPhone { get; set; }
        }



        public async Task<CompanyDto> GetCompanyByEmployerProfileIdAsync(int employerProfileId)
        {
            var company = await _context.Companies
                .Where(c => c.EmployerProfileId == employerProfileId)
                .Select(c => new CompanyDto
                {
                    EmployerProfileId = c.EmployerProfileId,
                    CompanyName = c.CompanyName,
                    CompanyDescription = c.CompanyDescription,
                    Industry = c.Industry,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    Country = c.Country,
                    PostalCode = c.PostalCode,
                    WebsiteUrl = c.WebsiteUrl,
                    ContactEmail = c.ContactEmail,
                    ContactPhone = c.ContactPhone
                })
                .FirstOrDefaultAsync();

            return company;
        }



        public async Task AddCompanyAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompanyAsync(Company updatedCompany)
        {
            var existingCompany = await _context.Companies
                .FirstOrDefaultAsync(c => c.EmployerProfileId == updatedCompany.EmployerProfileId);

            if (existingCompany != null)
            {
                existingCompany.CompanyName = updatedCompany.CompanyName ?? existingCompany.CompanyName;
                existingCompany.CompanyDescription = updatedCompany.CompanyDescription ?? existingCompany.CompanyDescription;
                existingCompany.Industry = updatedCompany.Industry ?? existingCompany.Industry;
                existingCompany.Address = updatedCompany.Address ?? existingCompany.Address;
                existingCompany.City = updatedCompany.City ?? existingCompany.City;
                existingCompany.State = updatedCompany.State ?? existingCompany.State;
                existingCompany.Country = updatedCompany.Country ?? existingCompany.Country;
                existingCompany.PostalCode = updatedCompany.PostalCode ?? existingCompany.PostalCode;
                existingCompany.WebsiteUrl = updatedCompany.WebsiteUrl ?? existingCompany.WebsiteUrl;
                existingCompany.ContactEmail = updatedCompany.ContactEmail ?? existingCompany.ContactEmail;
                existingCompany.ContactPhone = updatedCompany.ContactPhone ?? existingCompany.ContactPhone;
                existingCompany.CompanyPictureUrl = updatedCompany.CompanyPictureUrl ?? existingCompany.CompanyPictureUrl;

                _context.Companies.Update(existingCompany);
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddCompanyAsync(updatedCompany);
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }

}
