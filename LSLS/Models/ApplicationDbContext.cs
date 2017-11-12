namespace LSLS.Models
{
    using System.Data.Entity;

    public partial class ApplicationDbContext : DbContext
    {

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<TruckDriver> TruckDrivers { get; set; }

        public DbSet<TruckLocation> TruckLocations { get; set; }

        public DbSet<JobAssignment> JobAssignments { get; set; }

        public DbSet<TransportationInf> TransportationInfs { get; set; }

        public DbSet<TruckDriverDocument> TruckDriverDocuments { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }

        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
