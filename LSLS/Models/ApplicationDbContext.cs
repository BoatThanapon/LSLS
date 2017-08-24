namespace LSLS.Models
{
    using System.Data.Entity;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<TruckDriver> TruckDrivers { get; set; }
        public DbSet<TruckLocation> TruckLocations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
