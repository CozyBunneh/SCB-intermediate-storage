using Newtonsoft.Json;

namespace scb_api.Models.Queries
{
  public class ScbColumnQuery
  {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("selection")]
    public ScbFilterQuery Selection { get; set; }
  }
}