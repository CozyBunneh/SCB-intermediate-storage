using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  public class GenderFullV1
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<NewBornV1> NewBorns { get; set; }

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