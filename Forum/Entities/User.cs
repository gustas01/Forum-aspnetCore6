using Microsoft.AspNetCore.Identity;

namespace Forum.Entities;
public class User : IdentityUser {

  public List<Community>? CommunitiesMember { get; set; }
  public List<Community>? CommunitiesMod { get; set; }

  public List<Post>? Posts { get; set; }

  public string? AvatarUrl { get; set; }
}
