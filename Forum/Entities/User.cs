using Microsoft.AspNetCore.Identity;

namespace Forum.Entities;
internal class User : IdentityUser {
  public List<Community>? Communities { get; set; }

  public List<Post>? Posts { get; set; }

  public string? AvatarUrl { get; set; }
}
