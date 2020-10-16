using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scb_api.Models.Entities
{
  public class Region
  {
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<NewBorn> Borns { get; set; }
  }
}