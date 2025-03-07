using Microsoft.EntityFrameworkCore;
using offering_demo.Models;
using System.Collections.Generic;

namespace offering_demo.data
{
    public class COMSDbContext : DbContext
    {
      
            public COMSDbContext(DbContextOptions<COMSDbContext> options) : base(options)
            {
            }

        public DbSet<offering_demo.Models.Department> Department { get; set; }
        public DbSet<offering_demo.Models.Instructor> Instructor { get; set; }
        public DbSet<offering_demo.Models.Course> Course { get; set; }
        public DbSet<offering_demo.Models.Semester> Semester { get; set; }
        public DbSet<offering_demo.Models.Offering> Offering { get; set; }

    }
}
