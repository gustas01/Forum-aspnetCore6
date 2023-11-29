using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities;
[Index(nameof(Subject), IsUnique = true)]
public class Community {

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [Required]
  [StringLength(30, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
  public string? Subject { get; set; }

  [StringLength(150, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
  public string? Description { get; set; }

  public string? AvatarUrl { get; set; }

  public List<User>? UserMembers { get; set; } = new List<User>();
  public List<User>? UserMods { get; set; } = new List<User>();
  public List<Post>? Posts { get; set; } = new List<Post>();
}
