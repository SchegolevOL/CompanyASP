using Microsoft.EntityFrameworkCore;

namespace CompanyASP.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
