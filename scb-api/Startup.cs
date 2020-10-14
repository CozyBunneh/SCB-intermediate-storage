using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using scb_api.ApiClients;
using scb_api.Helpers;
using scb_api.Models;

namespace scb_api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      ScbNewBornApiClient = new ScbNewBornApiClient(Configuration);
    }

    private const string AllowSpecificOrigins = "_AllowSpecificOrigins";
    private readonly List<string> AllowLocalhost = new List<string>
    {
      "http://localhost:8080",
      "http://localhost"
    };

    public IConfiguration Configuration { get; }
    public ScbNewBornApiClient ScbNewBornApiClient { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Without this the fronend can't call the backend
      services.AddCors(options =>
      {
        options.AddPolicy(AllowSpecificOrigins, builder =>
        {
          builder.WithOrigins(AllowLocalhost.ToArray())
                 .AllowAnyHeader()
                 .AllowAnyMethod();
        });
      });

      services.AddControllers();

      services.AddSingleton<IConfiguration>(Configuration);
      services.AddSingleton<ScbNewBornApiClient>(ScbNewBornApiClient);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      UpdateDatabase(false, app.ApplicationServices);

      // Get data from SCB and populate the database with it
      PopulateDatabase(app.ApplicationServices);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    private void UpdateDatabase(bool delete, IServiceProvider serviceProvider)
    {
      using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ScbDbContext>();

        if (delete)
        {
          dbContext.Database.EnsureDeleted();
        }

        try
        {
          Directory.CreateDirectory(ScbHelper.GetScbDatabaseDirectory(Configuration));
          dbContext.Database.EnsureCreated();

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

    private void PopulateDatabase(IServiceProvider serviceProvider)
    {
      var scbTableResponse = ScbNewBornApiClient.GetNewBornPopulationTableInfo();
      var scbTableQueryResponse = ScbNewBornApiClient.PostNewBornPopulationQuery();

      using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<ScbDbContext>();
      }
    }
  }
}
