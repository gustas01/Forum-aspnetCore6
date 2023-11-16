using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities;
public class Post {

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [Required]
  [StringLength(80, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
  public string? Title { get; set; }

  [Required]
  public string? Content { get; set; }

  public User? UserPoster { get; set; }

  public Community? CommunityPoster { get; set; }
  public List<Comment>? Comments { get; set; }
}
