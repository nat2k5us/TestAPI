using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TesAPI.Helpers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
          : base(options)
        {
        }


        public virtual DbSet<Quote> Quotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1833;Initial Catalog=MyTestDB;User ID=sa;Password=Password12!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }
    }
    public class GraniteDBContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1833;Initial Catalog=MyTestDB;User ID=sa;Password=Password12!;");
            return new TestDbContext(optionsBuilder.Options);
        }
    }

}
