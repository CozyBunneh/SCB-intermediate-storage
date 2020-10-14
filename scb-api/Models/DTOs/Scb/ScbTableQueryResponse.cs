using System.Collections.Generic;
using Newtonsoft.Json;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableQueryResponse
  {
    [JsonProperty("data")]
    public IDictionary<string, int[]>[] Data { get; set; }
  }
}