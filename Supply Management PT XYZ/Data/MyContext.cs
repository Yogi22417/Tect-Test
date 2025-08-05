using Microsoft.EntityFrameworkCore;
using Supply_Management_PT_XYZ.Models.Entities;

namespace Supply_Management_PT_XYZ.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Akun> akun { get; set; }
        public DbSet<Vendor> vendor { get; set; }

    }
}
