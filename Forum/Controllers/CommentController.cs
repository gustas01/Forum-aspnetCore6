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

  [HttpPost("{postId:Guid}")]
  [Authorize]
  public async Task<ActionResult> Create(Guid postId, CreateCommentDTO createCommentDTO) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await commentService.Create(createCommentDTO, UserId, postId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }

  [HttpPut("{commentId:Guid}")]
  [Authorize]
  public async Task<ActionResult> Update(Guid commentId, UpdateCommentDTO updateCommentDTO) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await commentService.Update(updateCommentDTO, UserId, commentId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }
}
