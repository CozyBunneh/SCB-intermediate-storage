using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace scb_api.Models.Queries.Scb.Column.Filter
{
  public class ScbFilterQuery
  {
    [JsonProperty("filter")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ScbFilterTypes Filter { get; set; }

    [JsonProperty("values")]
    public string[] Values { get; set; }
  }
}