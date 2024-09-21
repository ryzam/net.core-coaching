

using DemoDatabaseFromScratch.Domains;
using Microsoft.EntityFrameworkCore;

namespace DemoDatabaseFromScratch.Datas
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        //All table variable name end with 's' 
        public DbSet<Organization> Organizations { get; set; } //Refer to Organization Table

        public DbSet<Employee> Employees { get; set; } //Refer to Employee Table

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //Fluent Database Design
        //    modelBuilder.Entity<Employee>()
        //       .HasOne(d => d.Organization)
        //       .WithMany()
        //       .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
