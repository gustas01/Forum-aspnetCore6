using Microsoft.AspNetCore.Identity;

namespace Forum.Entities;
public class User : IdentityUser {

  public List<Community>? CommunitiesAsMember { get; set; }

  public List<Community>? CommunitiesAsMod { get; set; }

  public List<Post>? Posts { get; set; }

  public List<Comment>? Comments { get; set; }

  public string? AvatarUrl { get; set; }
}
