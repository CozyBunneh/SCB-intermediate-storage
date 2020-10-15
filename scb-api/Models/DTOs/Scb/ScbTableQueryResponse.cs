using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.Scb
{
  public class ScbTableQueryResponse
  {
    private const string KeyKey = "key";
    private const string KeyValues = "values";
    private const int RegionIdIndex = 0;
    private const int GenderIdIndex = 1;
    private const int YearIndex = 2;
    private const int NewBornCountIndex = 0;

    [JsonProperty("data")]
    public IDictionary<string, string[]>[] Data { get; set; }

    public IEnumerable<Region> ToEntities(ScbTableResponse scbTableResponse)
    {
      var regions = scbTableResponse.ToRegionEntities();
      var gender = scbTableResponse.ToGenderEntities();

      foreach (var region in regions)
      {
        var regionData = Data.Where(d => d[KeyKey][RegionIdIndex] == region.Id).ToList();
        var newBorn = new List<Born>();
        foreach (var data in regionData)
        {
          newBorn.Add(new Born()
          {
            Gender = gender.FirstOrDefault(g => g.Id == int.Parse(data[KeyKey][GenderIdIndex])),
            Year = int.Parse(data[KeyKey][YearIndex]),
            Count = int.Parse(data[KeyValues][NewBornCountIndex])
          });
        }
        region.Born = newBorn;
      }

      return regions;
    }
  }
}