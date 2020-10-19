using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using scb_api.ApiClients;
using scb_api.Helpers;
using scb_api.Models;
using scb_api.Models.Entities;

namespace scb_api.Services
{
  /// <summary>
  /// Background service that updates and populates the databse.
  /// </summary>
  public class UpdateDatabaseHostedService : BackgroundService
  {
    private IServiceProvider _serviceProvider;
    private IConfiguration _configuration { get; }
    private ScbNewBornApiClient _scbNewBornApiClient { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="services"></param>
    public UpdateDatabaseHostedService(IServiceProvider services)
    {
      _serviceProvider = services;
      _configuration = services.GetRequiredService<IConfiguration>();
      _scbNewBornApiClient = new ScbNewBornApiClient(_configuration);
    }

    /// <summary>
    /// Removes the database and creates it a new, then calls the SCB api to populate
    /// it.
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      UpdateDatabase(true);
      await PopulateDatabase();
    }

    /// <summary>
    /// Not handled since there wont be any data in the database without the service
    /// completing.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
      await base.StopAsync(cancellationToken);
    }

    private void UpdateDatabase(bool delete)
    {
      using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ScbDbContext>();

        if (delete)
        {
          dbContext.Database.EnsureDeleted();
        }

        try
        {
          Directory.CreateDirectory(ScbHelper.GetScbDatabaseDirectory(_configuration));

          if (dbContext.Database.GetPendingMigrations().Any())
          {
            dbContext.Database.Migrate();
          }
        }
        catch (Exception)
        {
          throw;
        }
      }
    }

    private async Task PopulateDatabase()
    {
      var scbTableResponse = await _scbNewBornApiClient.GetNewBornPopulationTableInfo();
      var scbTableQueryResponse = await _scbNewBornApiClient.PostNewBornPopulationQuery();
      var regions = scbTableQueryResponse.ToEntities(scbTableResponse);

      using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ScbDbContext>();
        var regionDbSet = dbContext.Set<Region>();

        foreach (var region in regions)
        {
          await regionDbSet.AddAsync(region);
        }
        await dbContext.SaveChangesAsync();
      }
    }
  }
}