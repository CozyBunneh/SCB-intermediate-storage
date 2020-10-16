using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableResponse
  {
    private const string Kon = "Kon";
    private const string Region = "Region";

    [JsonProperty("variables")]
    public ScbTableInfoResponse<object>[] Variables { get; set; }

    public IEnumerable<Region> ToRegionEntities()
    {
      var regionInfo = Variables.FirstOrDefault(v => v.Code == Region);
      return regionInfo.Values.Zip(regionInfo.ValueTexts, (n, w) => new Region { Id = n.ToString(), Name = w }).ToList();
    }

    public IEnumerable<Gender> ToGenderEntities()
    {
      var genderInfo = Variables.FirstOrDefault(v => v.Code == Kon);
      return genderInfo.Values.Zip(genderInfo.ValueTexts, (n, w) => new Gender { Id = int.Parse(n.ToString()), Name = w }).ToList();
    }
  }
}