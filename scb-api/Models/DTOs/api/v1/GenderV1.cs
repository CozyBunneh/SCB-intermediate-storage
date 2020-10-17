using System.Collections.Generic;
using scb_api.Models.Entities;

namespace scb_api.Models.DTOs.api.v1
{
  public class GenderV1
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<int> NewBorns { get; set; }

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