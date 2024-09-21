using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;

namespace TalentTribe.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class JobSeekerProfileController : ControllerBase
    {
        private readonly IJobSeekerProfileRepository _repository;

        public JobSeekerProfileController(IJobSeekerProfileRepository repository)
        {
            _repository = repository;
        }

        // GET: api/JobSeekerProfile
        [HttpGet]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _repository.GetJobSeekerProfilesAsync();
            return Ok(profiles);
        }

        // GET: api/JobSeekerProfile/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _repository.GetJobSeekerProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        // POST: api/JobSeekerProfile
        [HttpPost]
        [Authorize(Roles = "Admin,JobSeeker")]

        public async Task<IActionResult> AddProfile([FromBody] JobSeekerProfile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddJobSeekerProfileAsync(profile);
            return CreatedAtAction(nameof(GetProfileById), new { id = profile.JobSeekerProfileId }, profile);
        }

        // PUT: api/JobSeekerProfile/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,JobSeeker")]

        public async Task<IActionResult> UpdateProfile(int id, [FromBody] JobSeekerProfile profile)
        {
            if (id != profile.JobSeekerProfileId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repository.UpdateJobSeekerProfileAsync(profile);
            return NoContent();
        }

        // DELETE: api/JobSeekerProfile/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _repository.GetJobSeekerProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            await _repository.DeleteJobSeekerProfileAsync(profile);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        // GET: api/JobSeekerProfile/5/Resume
        [HttpGet("{id}/Resume")]
        public async Task<IActionResult> DownloadResume(int id)
        {
            var resumePath = await _repository.DownloadResumeAsync(id);
            if (resumePath == null)
            {
                return NotFound();
            }

            return PhysicalFile(resumePath, "application/pdf");
        }
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        // GET: api/JobSeekerProfile/5/ProfilePicture
        [HttpGet("{id}/ProfilePicture")]
        public async Task<IActionResult> DownloadProfilePicture(int id)
        {
            var picturePath = await _repository.DownloadProfilePictureAsync(id);
            if (picturePath == null)
            {
                return NotFound();
            }

            return PhysicalFile(picturePath, "image/jpeg");
        }
    }
}
