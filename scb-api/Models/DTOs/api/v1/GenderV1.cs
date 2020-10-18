using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// Navigational model for gender data.
  /// Filtered by region already.
  /// </summary>
  public class GenderV1
  {
    /// <summary>
    /// Gender id.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Name of the gender
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// List of specific new born entries available for the specified region and gender
    /// by id.
    /// </summary>
    /// <value></value>
    public IEnumerable<int> NewBorns { get; set; }

    /// <summary>
    /// Translation method to translate from entity/persitent models to the API one.
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="newBorns"></param>
    /// <returns></returns>
    public static GenderV1 Translate(Gender gender, IEnumerable<int> newBorns = null)
    {
      return new GenderV1
      {
        Id = gender.Id,
        Name = gender.Name,
        NewBorns = newBorns
      };
    }
  }
}