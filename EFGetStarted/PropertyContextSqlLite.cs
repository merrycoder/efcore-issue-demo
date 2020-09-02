using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class PropertyContextSqlLite : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<CommuteTime> PostCommuteTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=properties.db");
    }
}