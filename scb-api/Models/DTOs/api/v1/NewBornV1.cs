using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  public class NewBornV1
  {
    public int Id { get; set; }
    public int Year { get; set; }
    public int Count { get; set; }

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