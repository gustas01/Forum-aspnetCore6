using Forum.DTOs;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase {
  private readonly PostService postService;

  public PostController(PostService postService) {
    this.postService = postService;
  }

  [HttpGet("{postId:Guid}")]
  public async Task<ActionResult> FindOne(Guid postId) {
    var result = await postService.FindOne(postId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }

  //[HttpGet("{postId:Guid}")]
  //public async Task<ActionResult> FindOneByUser(Guid postId) {

  //}

  [HttpPost("{communityId:Guid}")]
  [Authorize]
  public async Task<ActionResult> Create(CreatePostDTO createPostDTO, Guid communityId) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await postService.Create(createPostDTO, UserId, communityId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }


  [HttpPut("{postId:Guid}")]
  [Authorize]
  public async Task<ActionResult> Update(Guid postId, UpdatePostDTO updatePostDTO) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await postService.Update(updatePostDTO, UserId, postId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }


  [HttpDelete("{postId:Guid}")]
  [Authorize]
  public async Task<ActionResult> Delete(Guid postId) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await postService.Delete(UserId, postId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }
}
