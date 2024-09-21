using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TalentTribe.Models;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TalentTribeDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(TalentTribeDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Inject configuration to access Jwt Key
        }
        public class LoginDto
        {
            public string ?Username { get; set; }
            public string ?PasswordHash { get; set; }
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Find the user by username and password hash
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.PasswordHash);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Check the role and get additional profile information
            int? employerProfileId = null;
            int? jobSeekerProfileId = null;
            int? adminId = null;  // Add this for Admin role

            if (user.Role?.ToLower() == "employer")
            {
                // Fetch EmployerProfileId if the user is an employer
                var employerProfile = await _context.EmployerProfiles.FirstOrDefaultAsync(e => e.UserId == user.UserId);
                if (employerProfile != null)
                {
                    employerProfileId = employerProfile.EmployerProfileId;
                }
            }
            else if (user.Role?.ToLower() == "jobseeker")
            {
                // Fetch JobSeekerProfileId if the user is a job seeker
                var jobSeekerProfile = await _context.JobSeekerProfiles.FirstOrDefaultAsync(j => j.UserId == user.UserId);
                if (jobSeekerProfile != null)
                {
                    jobSeekerProfileId = jobSeekerProfile.JobSeekerProfileId;
                }
            }
            else if (user.Role?.ToLower() == "admin")
            {
                // Fetch AdminId if the user is an admin
                var adminProfile = await _context.Admins.FirstOrDefaultAsync(a => a.UserId == user.UserId);
                if (adminProfile != null)
                {
                    adminId = adminProfile.AdminId;
                }
            }

            // Generate the token
            var token = GenerateJwtToken(user, employerProfileId, jobSeekerProfileId, adminId);
            return Ok(new { token });
        }


        private string GenerateJwtToken(User user, int? employerProfileId, int? jobSeekerProfileId, int? adminId)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username!),
        new Claim("UserId", user.UserId.ToString()),
        new Claim("Role", user.Role!)
    };

            if (employerProfileId.HasValue)
            {
                claims.Add(new Claim("ProfileId", employerProfileId.Value.ToString()));
            }

            if (jobSeekerProfileId.HasValue)
            {
                claims.Add(new Claim("ProfileId", jobSeekerProfileId.Value.ToString()));
            }

            if (adminId.HasValue)
            {
                claims.Add(new Claim("ProfileId", adminId.Value.ToString()));
            }

            // Use _configuration to retrieve the JWT key from the configuration file
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }






        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Step 1: Check if the Username is already taken
            var existingUserByUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUserByUsername != null)
            {
                // Return a validation error if the username is already present
                return BadRequest(new { message = "Username is already taken." });
            }

            // Step 2: Check if the Email is already used by another user
            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUserByEmail != null)
            {
                // Return a validation error if the email is already present
                return BadRequest(new { message = "Email is already in use." });
            }

            // Step 3: Add the new user to the Users table
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Step 4: Create an EmployerProfile if the role is "Employer"
            if (user.Role?.ToLower() == "employer")
            {
                var employerProfile = new EmployerProfile
                {
                    UserId = user.UserId,  // Set the foreign key to the newly created UserId
                };
                _context.EmployerProfiles.Add(employerProfile);
            }

            // Step 5: Create a JobSeekerProfile if the role is "JobSeeker"
            if (user.Role?.ToLower() == "jobseeker")
            {
                var jobSeekerProfile = new JobSeekerProfile
                {
                    UserId = user.UserId,  // Set the foreign key to the newly created UserId
                };
                _context.JobSeekerProfiles.Add(jobSeekerProfile);
            }

            // Step 6: Save the changes for either EmployerProfile or JobSeekerProfile
            await _context.SaveChangesAsync();

            // Step 7: Return the created User object with a link to GetUser by UserId
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
