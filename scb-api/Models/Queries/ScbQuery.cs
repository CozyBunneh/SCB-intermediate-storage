using Newtonsoft.Json;

namespace scb_api.Models.Queries
{
  public class ScbQuery
  {
    [JsonProperty("query")]
    public ScbColumnQuery Query { get; set; }

    [JsonProperty("response")]
    public ScbResponseFormat Response { get; set; }
  }
}