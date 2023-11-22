using System.ComponentModel.DataAnnotations;

namespace Forum.DTOs {
  public class UpdateUserDTO {
    public string? UserName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Password { get; set; }
  }
}
