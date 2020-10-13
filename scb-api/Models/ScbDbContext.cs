using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using scb_api.Helpers;

namespace scb_api.Models
{
  public class ScbDbContext : DbContext
  {
    private const string DataSource = "Data Source";

    private IConfiguration configuration;

    protected ScbDbContext(IConfiguration configuration)
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

      // Crete composite keys (many-to-many)

      // Create composite keys (one-to-many)

      // Create unique indexes
    }
  }
}