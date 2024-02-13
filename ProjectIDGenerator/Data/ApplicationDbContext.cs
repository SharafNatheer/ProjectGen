using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectIDGenerator.Models;

namespace ProjectIDGenerator.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {}
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<ChangeRequests> ChangeRequests { get; set; }
    }
}
