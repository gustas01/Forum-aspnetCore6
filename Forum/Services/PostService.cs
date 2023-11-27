using AutoMapper;
using Forum.Context;
using Forum.DTOs;
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Services;
public class PostService {
  private readonly AppDbContext _context;
  private readonly UserManager<User> _userManager;
  private readonly IMapper _mapper;

  public PostService(AppDbContext context, UserManager<User> userManager, IMapper mapper) {
    _context = context;
    _mapper = mapper;
    _userManager = userManager;
  }

  public async Task<ActionResult<RequestResponseDTO>> Create(CreatePostDTO createPostDTO, string UserId, Guid communityId) {
    try {
      if(createPostDTO.Content == String.Empty || createPostDTO.Content == null || createPostDTO.Title == null || createPostDTO.Title == String.Empty)
        return new RequestResponseDTO() { Code = 400, Message = "Post vazio inválido!", Success = false };

      Post post = _mapper.Map<Post>(createPostDTO);
      post.UserPoster = await _userManager.FindByIdAsync(UserId);
      Community? community = await _context.Communities.FindAsync(communityId);

      //if(community == null)
      //  return new RequestResponseDTO() { Code = 404, Message = "Comunidade apagada ou inexistente!", Success = false };

      //post.CommunityPoster = community;

      _context.Posts.Add(post);
      await _context.SaveChangesAsync();
      return new RequestResponseDTO() { Code = 201, Message = "Post adicionado", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

}
