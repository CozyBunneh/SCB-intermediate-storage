using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace scb_api.Models.Queries
{
  public class ScbResponseFormat
  {
    [JsonProperty("format")]
    [JsonConverter(typeof(StringEnumConverter))]
    public string Format { get; set; }
  }
}