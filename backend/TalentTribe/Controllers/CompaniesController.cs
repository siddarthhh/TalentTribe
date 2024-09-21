using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;
using TalentTribe.Service;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies
        [HttpGet]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // GET: api/Companies/byEmployeeProfileId{employerProfileId}
        [HttpGet("byEmployeeProfileId{employerProfileId}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<IActionResult> GetCompanyByEmployerProfileId(int employerProfileId)
        {
            var company = await _companyService.GetCompanyByEmployerProfileIdAsync(employerProfileId);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            return Ok(company);
        }

        // POST or PUT: api/Companies/{employerProfileId}
        [HttpPost("{employerProfileId}")]
        [HttpPut("{employerProfileId}")]
        public async Task<IActionResult> UpsertCompany(int employerProfileId, [FromBody] Company updatedCompany)
        {
            if (updatedCompany == null )
            {
                return BadRequest("Invalid request parameters.");
            }

            if (await _companyService.GetCompanyByEmployerProfileIdAsync(employerProfileId) != null)
            {
                await _companyService.UpdateCompanyAsync(updatedCompany);
            }
            else
            {
                updatedCompany.EmployerProfileId = employerProfileId;
                await _companyService.AddCompanyAsync(updatedCompany);
            }

            return Ok(updatedCompany);
        }
        [Authorize(Roles = "Admin")]

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (!_companyService.CompanyExists(id))
            {
                return NotFound();
            }

            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}