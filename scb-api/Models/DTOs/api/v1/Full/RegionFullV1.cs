using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// The region modell intended for containing all of the region data.
  /// </summary>
  public class RegionFullV1
  {
    /// <summary>
    /// Region id.
    /// </summary>
    /// <value></value>
    public string Id { get; set; }

    /// <summary>
    /// Name of the region.
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// List of specific genders with corresponding new born data.
    /// </summary>
    /// <value></value>
    public IEnumerable<GenderFullV1> Genders { get; set; }

    /// <summary>
    /// Translation method to translate from entity/persitent models to the API one.
    /// </summary>
    /// <param name="region"></param>
    /// <param name="genders"></param>
    /// <returns></returns>
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