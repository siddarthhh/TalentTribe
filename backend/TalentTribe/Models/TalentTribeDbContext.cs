using Microsoft.EntityFrameworkCore;
using TalentTribe.Models;

namespace TalentTribe.Models
{
    public class TalentTribeDbContext : DbContext
    {
        public TalentTribeDbContext(DbContextOptions<TalentTribeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<JobSeekerProfile> JobSeekerProfiles { get; set; }
        public DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<JobSearchLog> JobSearchLogs { get; set; }
        public DbSet<AdminDashboardMetric> AdminDashboardMetrics { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Username and Email must be unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // JobSeekerProfile - Foreign Key UserId
            modelBuilder.Entity<JobSeekerProfile>()
                .HasOne(jsp => jsp.User)
                .WithMany(u => u.JobSeekerProfiles)
                .HasForeignKey(jsp => jsp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmployerProfile - Foreign Key UserId and CompanyId
            modelBuilder.Entity<EmployerProfile>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EmployerProfiles)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Cascade);

           

            // Admin - Foreign Key UserId
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithMany(u => u.Admins)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Credential - Foreign Key JobSeekerProfileId
            modelBuilder.Entity<Credential>()
                .HasOne(c => c.JobSeekerProfile)
                .WithMany(jsp => jsp.Credentials)
                .HasForeignKey(c => c.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Job - Foreign Key EmployerProfileId
            modelBuilder.Entity<Job>()
                .HasOne(j => j.EmployerProfile)
                .WithMany(ep => ep.Jobs)
                .HasForeignKey(j => j.EmployerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Application - Foreign Key JobSeekerProfileId and JobId
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobSeekerProfile)
                .WithMany(jsp => jsp.Applications)
                .HasForeignKey(a => a.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Restrict);


            // Interview - Foreign Key ApplicationId
            modelBuilder.Entity<Interview>()
                .HasOne(i => i.Application)
                .WithMany(a => a.Interviews)
                .HasForeignKey(i => i.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Communication - Foreign Key SenderId and ReceiverId (ReceiverId is nullable)
            modelBuilder.Entity<Communication>()
            .HasOne(c => c.Sender)
            .WithMany(u => u.SentCommunications)
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Communication>()
                .HasOne(c => c.Receiver)
                .WithMany(u => u.ReceivedCommunications)
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.SetNull);



            // JobSearchLog - Foreign Key JobSeekerProfileId
            modelBuilder.Entity<JobSearchLog>()
                .HasOne(jsl => jsl.JobSeekerProfile)
                .WithMany(jsp => jsp.JobSearchLogs)
                .HasForeignKey(jsl => jsl.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // AdminDashboardMetric - No Foreign Key
            modelBuilder.Entity<AdminDashboardMetric>()
                .Property(adm => adm.MetricName)
                .HasMaxLength(255)
                .IsRequired();

            // Report - Foreign Key GeneratedBy (Admin)
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Admin)
                .WithMany(a => a.Reports)
                .HasForeignKey(r => r.GeneratedBy)
                .OnDelete(DeleteBehavior.Cascade);

            // AuditLog - Foreign Key AdminId
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.Admin)
                .WithMany(a => a.AuditLogs)
                .HasForeignKey(al => al.AdminId)
                .OnDelete(DeleteBehavior.Cascade);

            // Company
            modelBuilder.Entity<Company>()
              .HasOne(c => c.EmployerProfile)
              .WithOne()  // One-to-one relationship
              .HasForeignKey<Company>(c => c.EmployerProfileId)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
