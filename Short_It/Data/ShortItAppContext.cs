using Microsoft.EntityFrameworkCore;
using Short_It.Models;

namespace Short_It.Data
{
    public class ShortItAppContext : DbContext
    {

        public ShortItAppContext(DbContextOptions<ShortItAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Link> Links { get; set; }

    }
}
