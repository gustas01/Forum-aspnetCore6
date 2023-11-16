using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities;
public class Community {

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [Required]
  [StringLength(30, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
  public string? Subject { get; set; }

  [StringLength(150, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
  public string? Description { get; set; }

  public string? AvatarUrl { get; set; }

  public List<User>? UserMembers { get; set; }
  public List<User>? UserMods { get; set; }
  public List<Post>? Posts { get; set; }
}
