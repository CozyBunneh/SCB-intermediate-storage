using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  public class RegionFullV1
  {
    public string Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<GenderFullV1> Genders { get; set; }

    public static RegionFullV1 Translate(Region region, IEnumerable<GenderFullV1> genders = null)
    {
      return new RegionFullV1
      {
        Id = region.Id,
        Name = region.Name,
        Genders = genders
      };
    }
  }
}