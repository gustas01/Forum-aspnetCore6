using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities {
  public class Comment {

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public User? User { get; set; }

    public Post? Post { get; set; }

    public string Content { get; set; }
  }
}
