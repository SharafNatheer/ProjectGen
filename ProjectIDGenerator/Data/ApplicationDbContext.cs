using Microsoft.EntityFrameworkCore;
using ProjectIDGenerator.Models;

namespace ProjectIDGenerator.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {}
        
        public DbSet<Project> Projects { get; set; }
    }
}
