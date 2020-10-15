using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using scb_api.Helpers;
using scb_api.Models.Entities;

namespace scb_api.Models
{
  public class ScbDbContext : DbContext
  {
    public DbSet<Region> Regions { get; set; }
    public DbSet<Born> Borns { get; set; }
    public DbSet<Gender> Genders { get; set; }

    private const string DataSource = "Data Source";

    private IConfiguration configuration;

    public ScbDbContext(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      options.UseSqlite($"{DataSource}={ScbHelper.GetScbDatabasePath(configuration)}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // Create composite keys (one-to-many)
      // builder.Entity<Born>().HasOne(b => b.Region).WithMany(r => r.Born);
      // builder.Entity<Born>().HasOne(b => b.Gender).WithMany(g => g.Born);

      // Create unique indexes
      // builder.Entity<Region>().HasIndex(r => r.Id).IsUnique();
      // builder.Entity<Born>().HasIndex(b => b.Id).IsUnique();
      // builder.Entity<Gender>().HasIndex(g => g.Id).IsUnique();
    }
  }
}