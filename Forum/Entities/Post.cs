using System.ComponentModel.DataAnnotations;

namespace Forum.Entities;
internal class Post {

  [Required]
  public string? Title { get; set; }

  [Required]
  public string? Content { get; set; }

  public User? UserPoster { get; set; }

  public Community? CommunityPoster { get; set; }
}
