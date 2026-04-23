


using Helloworld.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Helloworld.Data
{
    public class DataContextEF : DbContext
    {

        private IConfiguration _config;

        private string _connectionString;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }



        public DbSet<Computer>? computer {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                // .HasNoKey();
                .HasKey(c => c.ComputerId);

                // .ToTable("Computer","TutorialAppSchema");
                // .ToTable("TableName","SchemaName");

        }

    }
    }