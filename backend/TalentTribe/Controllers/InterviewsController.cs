using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

namespace TalentTribe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly TalentTribeDbContext _context;

        public InterviewsController(TalentTribeDbContext context)
        {
            _context = context;
        }

        // GET: api/Interviews
        [HttpGet]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<IEnumerable<Interview>>> GetInterviews()
        {
            return await _context.Interviews.ToListAsync();
        }


        // GET: api/interviews/application/{applicationId}
        [HttpGet("application/{applicationId}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public IActionResult GetInterviewByApplicationId(int applicationId)
        {
            var interview = _context.Interviews
                .FirstOrDefault(i => i.ApplicationId == applicationId);

            if (interview == null)
            {
                return NotFound(new { Message = "Interview not found for the given application." });
            }

            return Ok(interview);
        }

        // GET: api/Interviews/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<ActionResult<Interview>> GetInterview(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);

            if (interview == null)
            {
                return NotFound();
            }

            return interview;
        }

        // PUT: api/Interviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employer,JobSeeker")]

        public async Task<IActionResult> PutInterview(int id, Interview interview)
        {
            if (id != interview.InterviewId)
            {
                return BadRequest();
            }

            // Mark the entity as modified
            _context.Entry(interview).State = EntityState.Modified;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Send interview notification asynchronously and handle exceptions
                try
                {
                    await SendInterviewNotificationByApplicationId(interview.ApplicationId);
                }
                catch (Exception ex)
                {
                    // Log the error for debugging purposes
                    Console.WriteLine($"Failed to send interview notification: {ex.Message}");
                    // You can decide to handle this error further if needed
                }

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Interviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin,Employer")]

        public async Task<ActionResult<Interview>> PostInterview(Interview interview)
        {
            // Add the new interview to the context
            _context.Interviews.Add(interview);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Try sending interview notification asynchronously and handle exceptions
            try
            {
                await SendInterviewNotificationByApplicationId(interview.ApplicationId);
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes (logging framework is preferable)
                Console.WriteLine($"Failed to send interview notification: {ex.Message}");
                // You can also decide whether to return an error response or continue
            }

            // Return the created interview with a 201 Created response
            return NoContent();
        }

        private async Task SendInterviewNotificationByApplicationId(int applicationId)
        {
            try
            {
                // Step 1: Retrieve the interview related to the Job Seeker
                var jobSeekerInterview = await _context.Interviews
                    .AsNoTracking()
                    .Include(i => i.Application)
                    .ThenInclude(a => a.JobSeekerProfile)
                    .ThenInclude(js => js.User)
                    .FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

                if (jobSeekerInterview == null)
                {
                    Console.WriteLine("Job Seeker Interview not found.");
                    return; // Exit early if no data found
                }

                // Ensure all the necessary data is present
                var jobSeekerProfile = jobSeekerInterview.Application?.JobSeekerProfile;
                var jobSeekerUser = jobSeekerProfile?.User;

                if (jobSeekerProfile == null || jobSeekerUser == null)
                {
                    Console.WriteLine("Job Seeker Profile or User data is incomplete.");
                    return;
                }

                // Step 2: Retrieve the interview related to the Employer
                var employerInterview = await _context.Interviews
                    .AsNoTracking()
                    .Include(i => i.Application)
                    .ThenInclude(a => a.Job)
                    .ThenInclude(j => j.EmployerProfile)
                    .ThenInclude(ep => ep.Company)
                    .FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

                if (employerInterview == null)
                {
                    Console.WriteLine("Employer interview data not found.");
                    return;
                }

                // Step 3: Check and send the email if valid data is present
                var jobSeekerEmail = jobSeekerUser.Email;

                if (!string.IsNullOrEmpty(jobSeekerEmail))
                {
                    await SendInterviewScheduledEmail(jobSeekerEmail, employerInterview);
                    Console.WriteLine($"Interview scheduled email sent to {jobSeekerEmail}");
                }
                else
                {
                    throw new Exception("Job Seeker email is empty or null.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for better debugging
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private async Task SendInterviewScheduledEmail(string email, Interview interview)
        {
            var fromAddress = new MailAddress("talenttribe30@gmail.com", "TalentTribe");
            var toAddress = new MailAddress(email);
            const string fromPassword = "qswl dgez gvaw wlsb";
            const string subject = "Interview Scheduled Notification";

            // Retrieve job title and company name from the interview's associated application and job
            var jobTitle = interview.Application?.Job?.JobTitle ?? "N/A"; // Fallback to "N/A" if job title is null
            var companyName = interview.Application?.Job?.companyName ?? "N/A"; // Fallback to "N/A" if company name is null

            // Creating a dynamic body message with interview details
            var body = $"Dear Applicant,\n\n" +
                       $"We are pleased to inform you that an interview has been scheduled for the position: {jobTitle} at {companyName}. Below are the details of the interview:\n\n" +
                       $"Interview Type: {interview.InterviewType}\n" +
                       $"Interview Date: {interview.InterviewDate:MMMM dd, yyyy hh:mm tt}\n" +
                       $"Interview Location: {interview.InterviewLocation ?? "N/A"}\n" +
                       $"Interview Link (for remote interviews): {interview.InterviewLink ?? "N/A"}\n\n" +
                       $"Please ensure that you are available at the specified time. We look forward to meeting with you.\n\n" +
                       $"Best Regards,\nTalentTribe Team";

            // SMTP client setup for sending the email
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            // Creating and sending the email message
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }


        // DELETE: api/Interviews/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteInterview(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.InterviewId == id);
        }
    }
}
