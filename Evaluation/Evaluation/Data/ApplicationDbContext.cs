using Evaluation.Models;
using Microsoft.EntityFrameworkCore;

namespace Evaluation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Salary> salaries { get; set; }
    }
}
