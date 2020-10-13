using System.Collections.Generic;
using Newtonsoft.Json;
using scb_api.Models.Queries.Scb.Column;
using scb_api.Models.Queries.Scb.Response;

namespace scb_api.Models.Queries.Scb
{
  public class ScbQuery
  {
    [JsonProperty("query")]
    public List<ScbColumnQuery> Query { get; set; }

    [JsonProperty("response")]
    public ScbResponseFormat Response { get; set; }
  }
}