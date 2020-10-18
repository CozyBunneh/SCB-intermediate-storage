using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// The gender modell intended for containing all of the new born data for that gender.
  /// </summary>
  public class GenderFullV1
  {
    /// <summary>
    /// Id for the gender.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Name of the gender.
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// New born data for the gender, filtered for the selected region.
    /// </summary>
    /// <value></value>
    public IEnumerable<NewBornV1> NewBorns { get; set; }

    /// <summary>
    /// Translation method to translate from entity/persitent models to the API one.
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="newBorns"></param>
    /// <returns></returns>
    public static GenderFullV1 Translate(Gender gender, IEnumerable<NewBornV1> newBorns = null)
    {
      return new GenderFullV1
      {
        Id = gender.Id,
        Name = gender.Name,
        NewBorns = newBorns
      };
    }
  }
}