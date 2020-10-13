using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using scb_api.ApiClients;

namespace scb_api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      ScbNewBornApiClient = new ScbNewBornApiClient(Configuration);
      ScbNewBornApiClient.PostNewBornPopulationQuery();
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

      //   services.AddSingleton<IMyService>((container) =>
      // {
      //     var logger = container.GetRequiredService<ILogger<MyService>>();
      //     return new MyService() { Logger = logger };
      // });
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

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
