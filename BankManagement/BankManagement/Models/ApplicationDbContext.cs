using Microsoft.EntityFrameworkCore;

namespace BankManagement.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Example DbSet
        public DbSet<Users> Users { get; set; }
    }
}
