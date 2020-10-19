using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using scb_api.Models;
using scb_api.Services;

namespace scb_api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private const string AllowSpecificOrigins = "_AllowSpecificOrigins";
    private readonly List<string> AllowLocalhost = new List<string>
    {
      "http://localhost:8080",
      "http://localhost"
    };

    public IConfiguration Configuration { get; }

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

      services.AddDbContext<ScbDbContext>();

      services.AddControllers((options) =>
      {
        options.CacheProfiles.Add("Default", new CacheProfile()
        {
          Duration = 60,
          Location = ResponseCacheLocation.Any,
          NoStore = false
        });
      });
      services.AddResponseCaching();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "SCB intermediate storage API",
          Version = "v1",
          Description = "Intermediate storage API for Statistics Sweden",
        });

        // XML Documentation
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
      services.AddSwaggerGenNewtonsoftSupport();

      services.AddSingleton<IConfiguration>(Configuration);
      services.AddHostedService<UpdateDatabaseHostedService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(AllowSpecificOrigins);

      app.UseHttpsRedirection();

      app.UseResponseCaching();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCB intermediate storage API v1");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
