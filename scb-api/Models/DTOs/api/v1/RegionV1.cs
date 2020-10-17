using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// Navigational model for region
  /// </summary>
  public class RegionV1
  {
    public string Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<int> Genders { get; set; }

    public static RegionV1 Translate(Region region, IEnumerable<int> genders = null)
    {
      return new RegionV1
      {
        Id = region.Id,
        Name = region.Name,
        Genders = genders
      };
    }
  }
}