using AutoMapper;
using Forum.Context;
using Forum.DTOs;
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Services;

public class CommentService {
  private readonly AppDbContext _context;
  private readonly UserManager<User> _userManager;
  private readonly IMapper _mapper;
  public CommentService(AppDbContext context, UserManager<User> userManager, IMapper mapper) {
    _context = context;
    _mapper = mapper;
    _userManager = userManager;
  }

  public async Task<ActionResult<RequestResponseDTO>> Create(CreateCommentDTO createCommentDTO, string UserId, Guid postId) {
    if(createCommentDTO == null || createCommentDTO.Content == String.Empty)
      return new RequestResponseDTO() { Code = 400, Message = "Comentário vazio inválido!", Success = false };

    Comment comment = _mapper.Map<Comment>(createCommentDTO);
    comment.User = await _userManager.FindByIdAsync(UserId);
    Post? post = await _context.Posts.FindAsync(postId);
    if(post == null)
      return new RequestResponseDTO() { Code = 404, Message = "Post apagado ou inexistente!", Success = false };
    comment.Post = post;

    _context.Comments.Add(comment);
    await _context.SaveChangesAsync();

    return new RequestResponseDTO() { Code = 201, Message = "Comentário adicionado", Success = true };
  }
  //public async Task<ActionResult> Create() { }
  //public async Task<ActionResult> Create() { }
  //public async Task<ActionResult> Create() { }
}
