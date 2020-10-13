using Newtonsoft.Json;
using scb_api.Models.Queries.Scb.Column.Filter;

namespace scb_api.Models.Queries.Scb.Column
{
  public class ScbColumnQuery
  {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("selection")]
    public ScbFilterQuery Selection { get; set; }
  }
}