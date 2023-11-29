using Microsoft.AspNetCore.Identity;

namespace Forum.Entities;
public class User : IdentityUser {

  public List<Community>? CommunitiesAsMember { get; set; } = new List<Community>();

  public List<Community>? CommunitiesAsMod { get; set; } = new List<Community>();

  public List<Post>? Posts { get; set; } = new List<Post>();

  public List<Comment>? Comments { get; set; } = new List<Comment>();

  public string? AvatarUrl { get; set; }
}
