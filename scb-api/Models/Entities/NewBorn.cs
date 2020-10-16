using System.ComponentModel.DataAnnotations;

namespace scb_api.Models.Entities
{
  public class NewBorn
  {
    [Key]
    public int Id { get; set; }
    public int Year { get; set; }
    public int Count { get; set; }

    public Region Region { get; set; }
    public Gender Gender { get; set; }
  }
}