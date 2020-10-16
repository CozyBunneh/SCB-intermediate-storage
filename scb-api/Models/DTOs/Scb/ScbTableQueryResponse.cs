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
      var genders = scbTableResponse.ToGenderEntities();

      // var regionsPopulated = new List<Region>();
      foreach (var region in regions)
      {
        var regionData = Data.Where(d => d[KeyKey][RegionIdIndex] == region.Id).ToList();
        var newBorns = new List<NewBorn>();
        foreach (var data in regionData)
        {
          var gender = genders.FirstOrDefault(g => g.Id == int.Parse(data[KeyKey][GenderIdIndex]));

          var born = new NewBorn()
          {
            // Gender = gender.Id,
            Year = int.Parse(data[KeyKey][YearIndex]),
            Count = int.Parse(data[KeyValues][NewBornCountIndex]),
            Region = region,
            Gender = genders.FirstOrDefault(g => g.Id == int.Parse(data[KeyKey][GenderIdIndex])),
          };

          newBorns.Add(born);
        }
        region.Borns = newBorns;
      }

      return regions;
    }
  }
}