using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities;
internal class Community {

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [Required]
  public string? Subject { get; set; }
  public string? Description { get; set; }
  public string? AvatarUrl { get; set; }
}
