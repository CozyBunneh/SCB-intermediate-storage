using System.Runtime.Serialization;

namespace scb_api.Models.Queries
{
  public enum ScbResponseFormatTypes
  {
    [EnumMember(Value = "px")]
    Px,
    [EnumMember(Value = "csv")]
    Csv,
    [EnumMember(Value = "json")]
    Json,
    [EnumMember(Value = "xlsx")]
    Xlsx,
    [EnumMember(Value = "json-stat")]
    JsonStat,
    [EnumMember(Value = "sdmx")]
    Sdmx
  }
}