using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using scb_api.Helpers;
using scb_api.Models.DTOs.Scb;
using scb_api.Models.Queries.Scb;
using scb_api.Models.Queries.Scb.Column;
using scb_api.Models.Queries.Scb.Column.Filter;
using scb_api.Models.Queries.Scb.Response;

namespace scb_api.ApiClients
{
  public class ScbNewBornApiClient : ApiClientBase<ScbNewBornApiClient>
  {
    private static string Region = "Region";
    private static string Kon = "Kon";
    private static string Tid = "Tid";
    private static string Wildcard = "*";
    private static string S2010 = "2010";
    private static string S2011 = "2011";
    private static string S2012 = "2012";
    private static string S2013 = "2013";
    private static string S2014 = "2014";

    private string _apiEndpoint = $"{ScbHelper.Population}/{ScbHelper.PopulationStatistics}/{ScbHelper.LiveBirths}/{ScbHelper.LiveBirthsByRegionMothersAgeChildSexAndYear}";

    public ScbNewBornApiClient(IConfiguration configuration) : base(configuration, ScbHelper.GetScbUri(configuration)) { }

    public ScbTableResponse GetNewBornPopulationTableInfo()
    {
      var response = GetAsync(_apiEndpoint).Result;
      var json = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<ScbTableResponse>(json);
    }

    public ScbTableQueryResponse PostNewBornPopulationQuery()
    {
      var query = new ScbQuery()
      {
        Query = new List<ScbColumnQuery>() {
          new ScbColumnQuery() {
            Code = Region,
            Selection = new ScbFilterQuery() {
              Filter = ScbFilterTypes.All,
              Values = new string[] { Wildcard }
            }
          },
          new ScbColumnQuery() {
            Code = Kon,
            Selection = new ScbFilterQuery() {
              Filter = ScbFilterTypes.All,
              Values = new string[] { Wildcard }
            }
          },
          new ScbColumnQuery() {
            Code = Tid,
            Selection = new ScbFilterQuery() {
              Filter = ScbFilterTypes.Item,
              Values = new string[] { S2010, S2011, S2012, S2013, S2014 }
            }
          }
        },
        Response = new ScbResponseFormat()
        {
          Format = ScbResponseFormatTypes.Json
        }
      };

      var response = PostAsync(_apiEndpoint, query).Result;
      var json = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<ScbTableQueryResponse>(json);
    }
  }
}