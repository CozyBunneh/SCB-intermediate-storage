using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// Navigational model for region data.
  /// </summary>
  public class RegionV1
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
    /// List of specific gender ids available for the region.
    /// </summary>
    /// <value></value>
    public IEnumerable<int> Genders { get; set; }

    /// <summary>
    /// Translation method to translate from entity/persitent models to the API one.
    /// </summary>
    /// <param name="region"></param>
    /// <param name="genders"></param>
    /// <returns></returns>
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