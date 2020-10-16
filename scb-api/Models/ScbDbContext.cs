using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using scb_api.Helpers;
using scb_api.Models.Entities;

namespace scb_api.Models
{
  public class ScbDbContext : DbContext
  {
    public DbSet<Region> Regions { get; set; }
    public DbSet<NewBorn> Borns { get; set; }
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
    }
  }
}