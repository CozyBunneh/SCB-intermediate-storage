using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scb_api.Models.Entities
{
  public class Gender
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual IEnumerable<Born> Born { get; set; }
  }
}