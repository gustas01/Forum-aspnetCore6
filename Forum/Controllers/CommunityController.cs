using Forum.DTOs;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommunityController : ControllerBase {
  private readonly CommunityService _communityService;

  public CommunityController(CommunityService communityService) {
    _communityService = communityService;
  }

  [HttpGet("{communityId:Guid}")]
  public async Task<ActionResult> FindOne(Guid communityId) {
    var result = await _communityService.FindOne(communityId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult> Create(CreateCommunityDTO createCommunityDTO) {
    string? UserId = HttpContext.User.FindFirst("Jti")?.Value;
    var result = await _communityService.Create(createCommunityDTO, UserId);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }
}
