using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableInfoResponse<T>
  {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("values")]
    public T[] Values { get; set; }

    [JsonProperty("valueTexts")]
    public string[] ValueTexts { get; set; }
  }
}