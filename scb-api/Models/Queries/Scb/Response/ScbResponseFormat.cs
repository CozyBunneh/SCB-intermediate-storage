using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace scb_api.Models.Queries.Scb.Response
{
  public class ScbResponseFormat
  {
    [JsonProperty("format")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ScbResponseFormatTypes Format { get; set; }
  }
}