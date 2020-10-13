using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using scb_api.Helpers;

namespace scb_api.ApiClients
{
  public class ScbNewBornApiClient : ApiClientBase<ScbNewBornApiClient>
  {
    public ScbNewBornApiClient(IConfiguration configuration, ILogger<ScbNewBornApiClient> logger) : base(configuration, logger, ScbHelper.GetScbUri(configuration)) { }

    public void GetNewBornPopulation()
    {
      var apiEndpoint = $"{ScbHelper.Population}/{ScbHelper.PopulationStatistics}/{ScbHelper.LiveBirths}/{ScbHelper.LiveBirthsByRegionMothersAgeChildSexAndYear}";

      var query = new Dictionary<string, string>();
      // query.Add()
    }
  }
}