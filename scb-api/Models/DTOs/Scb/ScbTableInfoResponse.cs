using System;
using Newtonsoft.Json;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableInfoResponse<T>
  {
    // private const string Region = "Region";
    // private const string AlderModer = "AlderModer";
    // private const string Kon = "Kon";
    // private const string ContentsCode = "ContentsCode";
    // private const string Tid = "Tid";

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("values")]
    public T[] Values { get; set; }

    [JsonProperty("valueTexts")]
    public string[] ValueTexts { get; set; }

    /// <summary>
    /// TODO: Change this
    /// </summary>
    /// <returns></returns>
    public Type IdentifyType()
    {
      if (Enum.TryParse(Code, out ScbTableInfoResponseTypes type))
      {
        switch (type)
        {
          case ScbTableInfoResponseTypes.Region:
            return typeof(string);
          case ScbTableInfoResponseTypes.AlderModer:
            return typeof(string);
          case ScbTableInfoResponseTypes.Kon:
            return typeof(int);
          case ScbTableInfoResponseTypes.ContentsCode:
            return typeof(string);
          case ScbTableInfoResponseTypes.Tid:
            return typeof(int);
        }
      }

      return typeof(object);
    }
  }
}