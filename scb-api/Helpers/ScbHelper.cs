using System;
using Microsoft.Extensions.Configuration;

namespace scb_api.Helpers
{
  public class ScbHelper
  {
    private const string ScbConfig = "ScbConfig";
    private const string Protocol = "Protocol";
    private const string Address = "Address";
    private const string ApiVersion = "ApiVersion";
    private const string ApiType = "ApiType";
    private const string Language = "Language";
    private const string DatabaseId = "DatabaseId";
    private const string LocalSqLitePath = "LocalSqLitePath";

    public const string Population = "BE";
    public const string PopulationStatistics = "BE0101";
    public const string LiveBirths = "BE0101H";
    public const string LiveBirthsByRegionMothersAgeChildSexAndYear = "FoddaK";

    public static Uri GetScbUri(IConfiguration configuration)
    {
      var scbConfig = configuration.GetSection(ScbHelper.ScbConfig);

      var protocol = scbConfig.GetValue<string>(ScbHelper.Protocol);
      var address = scbConfig.GetValue<string>(ScbHelper.Address);
      var apiVersion = scbConfig.GetValue<string>(ScbHelper.ApiVersion);
      var apiType = scbConfig.GetValue<string>(ScbHelper.ApiType);
      var language = scbConfig.GetValue<string>(ScbHelper.Language);
      var databaseId = scbConfig.GetValue<string>(ScbHelper.DatabaseId);

      return new Uri($"{protocol}://{address}/{apiVersion}/{apiType}/{language}/{databaseId}");
    }

    public static string GetScbDatabasePath(IConfiguration configuration)
    {
      var scbConfig = configuration.GetSection(ScbConfig);
      return scbConfig.GetValue<string>(LocalSqLitePath);
    }
  }
}