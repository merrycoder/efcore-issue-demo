using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class PropertyContextInMemory : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<CommuteTime> PostCommuteTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase(databaseName: "Properties");
    }
}