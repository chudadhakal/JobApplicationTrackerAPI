using JobApplicationTracker.WebApi.Models;
using Microsoft.EntityFrameworkCore;


namespace JobApplicationTracker.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<User> Users { get; set; }
    }
}