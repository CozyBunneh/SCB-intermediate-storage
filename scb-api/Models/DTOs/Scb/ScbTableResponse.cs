using Newtonsoft.Json;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableResponse
  {
    [JsonProperty("variables")]
    public ScbTableInfoResponse<object>[] Variables { get; set; }
  }
}