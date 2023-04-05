using Microsoft.EntityFrameworkCore;

namespace TestCQRS.Models
{
    public class CqrsContext : DbContext
    {
        public CqrsContext(DbContextOptions<CqrsContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
