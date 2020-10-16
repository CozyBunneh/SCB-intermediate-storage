using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scb_api.Models.Entities
{
  public class Gender
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<NewBorn> Borns { get; set; }
  }
}