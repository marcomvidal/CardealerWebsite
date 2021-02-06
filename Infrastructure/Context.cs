using Cars.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) {}

        public DbSet<Car> Cars { get; set; }
    }
}