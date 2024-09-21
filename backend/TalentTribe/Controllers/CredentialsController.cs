using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employer,JobSeeker")]

    public class CredentialsController : ControllerBase
    {
        private readonly TalentTribeDbContext _context;

        public CredentialsController(TalentTribeDbContext context)
        {
            _context = context;
        }

        // GET: api/Credentials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credential>>> GetCredentials()
        {
            return await _context.Credentials.ToListAsync();
        }

        // GET: api/Credentials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credential>> GetCredential(int id)
        {
            var credential = await _context.Credentials.FindAsync(id);

            if (credential == null)
            {
                return NotFound();
            }

            return credential;
        }
        public class CredentialUploadModel
        {
            public string? CredentialName { get; set; }
            public string? IssuedBy { get; set; }
            public DateTime? IssueDate { get; set; }
            public string? CredentialType { get; set; }
            public int JobSeekerProfileId { get; set; }

            [FromForm(Name = "file")]
            public IFormFile File { get; set; }
        }

        // POST: api/Credentials/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCredentialPdf([FromForm] CredentialUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded." });
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedCredentials");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Path.GetFileName(model.File.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var credential = new Credential
            {
                CredentialName = model.CredentialName,
                CredentialUrl = $"/UploadedCredentials/{fileName}",
                IssuedBy = model.IssuedBy,
                IssueDate = model.IssueDate,
                CredentialType = model.CredentialType,
                JobSeekerProfileId = model.JobSeekerProfileId
            };

            _context.Credentials.Add(credential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredential", new { id = credential.CredentialId }, credential);
        }

        // PUT: api/Credentials/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCredential(int id, [FromForm] CredentialUploadModel model)
        {
            var existingCredential = await _context.Credentials.FindAsync(id);
            if (existingCredential == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.CredentialName))
            {
                existingCredential.CredentialName = model.CredentialName;
            }
            if (!string.IsNullOrEmpty(model.IssuedBy))
            {
                existingCredential.IssuedBy = model.IssuedBy;
            }
            if (model.IssueDate.HasValue)
            {
                existingCredential.IssueDate = model.IssueDate;
            }
            if (!string.IsNullOrEmpty(model.CredentialType))
            {
                existingCredential.CredentialType = model.CredentialType;
            }

            if (model.File != null && model.File.Length > 0)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedCredentials");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (!string.IsNullOrEmpty(existingCredential.CredentialUrl))
                {
                    var existingFilePath = Path.Combine(uploadPath, Path.GetFileName(existingCredential.CredentialUrl));
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }

                var fileName = Path.GetFileName(model.File.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                existingCredential.CredentialUrl = $"/UploadedCredentials/{fileName}";
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // GET: api/Credentials/download/{id}
        [HttpGet("download/{id}")]
        public IActionResult DownloadCredential(int id)
        {
            var credential = _context.Credentials.FirstOrDefault(c => c.CredentialId == id);
            if (credential == null)
            {
                return NotFound("Credential not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), credential.CredentialUrl.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Credential file not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var mimeType = "application/pdf"; // Since credentials are PDFs

            return new FileStreamResult(fileStream, mimeType)
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }

        // List credentials for the logged-in job seeker
        [HttpGet("jobseeker/{jobSeekerProfileId}")]
        public IActionResult GetJobSeekerCredentials(int jobSeekerProfileId)
        {
            var credentials = _context.Credentials
                .Where(c => c.JobSeekerProfileId == jobSeekerProfileId)
                .ToList();

            if (credentials == null || credentials.Count == 0)
            {
                return NotFound("No credentials found for this profile.");
            }

            return Ok(credentials);
        }

        // DELETE: api/Credentials/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredential(int id)
        {
            var credential = await _context.Credentials.FindAsync(id);
            if (credential == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(credential.CredentialUrl))
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedCredentials");
                var filePath = Path.Combine(uploadPath, Path.GetFileName(credential.CredentialUrl));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Credentials.Remove(credential);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CredentialExists(int id)
        {
            return _context.Credentials.Any(e => e.CredentialId == id);
        }
    }
}
