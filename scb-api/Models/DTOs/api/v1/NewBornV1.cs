using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  /// <summary>
  /// Model for new born data.
  /// </summary>
  public class NewBornV1
  {
    /// <summary>
    /// New born entity id.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// New born entity year.
    /// </summary>
    /// <value></value>
    public int Year { get; set; }

    /// <summary>
    /// New born count.
    /// </summary>
    /// <value></value>
    public int Count { get; set; }

    /// <summary>
    /// Translation method to translate from entity/persitent models to the API one.
    /// </summary>
    /// <param name="newBorn"></param>
    /// <returns></returns>
    public static NewBornV1 Translate(NewBorn newBorn)
    {
      return new NewBornV1
      {
        Id = newBorn.Id,
        Year = newBorn.Year,
        Count = newBorn.Count
      };
    }
  }
}