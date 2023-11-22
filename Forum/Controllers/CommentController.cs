using Forum.DTOs;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase {
  private readonly CommentService commentService;
  public CommentController(CommentService commentService) {
    this.commentService = commentService;
  }

  [HttpPost("{postId}")]
  [Authorize]
  public async Task<ActionResult> Create(CreateCommentDTO createCommentDTO) {
    string? UserID = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await commentService.Create(createCommentDTO, UserID);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }
}
