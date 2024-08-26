using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Database
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
