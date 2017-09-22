namespace LSLS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<TruckDriver> TruckDrivers { get; set; }
        public DbSet<TruckLocation> TruckLocations { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }
        public DbSet<TransportationInf> TransportationInfs { get; set; }

        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
