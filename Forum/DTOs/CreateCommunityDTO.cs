using System.ComponentModel.DataAnnotations;

namespace Forum.DTOs;
public class CreateCommunityDTO {
  public string? Subject { get; set; }

  [StringLength(150, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
  public string? Description { get; set; }
}
