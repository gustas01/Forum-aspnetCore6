using AutoMapper;
using Forum.Context;
using Forum.DTOs;
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    try {
      if(createCommentDTO == null || createCommentDTO.Content == String.Empty)
        return new RequestResponseDTO() { Code = 400, Message = "Comentário vazio inválido!", Success = false };

      Comment comment = _mapper.Map<Comment>(createCommentDTO);
      comment.User = await _userManager.FindByIdAsync(UserId);
      Post? post = await _context.Posts.FindAsync(postId);
      //if(post == null)
      //  return new RequestResponseDTO() { Code = 404, Message = "Post apagado ou inexistente!", Success = false };
      //comment.Post = post;

      _context.Comments.Add(comment);
      await _context.SaveChangesAsync();

      return new RequestResponseDTO() { Code = 201, Message = "Comentário adicionado", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

  public async Task<ActionResult<RequestResponseDTO>> Update(UpdateCommentDTO updateCommentDTO, string? userId, Guid? commentId) {
    try {
      if(updateCommentDTO == null || updateCommentDTO.Content == String.Empty)
        return new RequestResponseDTO() { Code = 400, Message = "Comentário vazio inválido!", Success = false };

      Comment? comment = await _context.Comments.Include(c => c.User).SingleOrDefaultAsync(c => c.Id == commentId);

      //verificando se um usuário diferente está tentando modificar comentário do atual
      if(comment.User.Id != userId)
        return new RequestResponseDTO() { Code = 401, Message = "Impossível alterar comentário de outro usuário", Success = false };

      _mapper.Map(updateCommentDTO, comment);
      _context.Entry(comment).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return new RequestResponseDTO() { Code = 200, Message = "Comentário atualizado com sucesso!", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }



  public async Task<ActionResult<RequestResponseDTO>> Delete(string? userId, Guid? commentId) {
    try {
      Comment? comment = await _context.Comments.Include(c => c.User).SingleOrDefaultAsync(c => c.Id == commentId);

      //verificando se um usuário diferente está tentando modificar comentário do atual
      if(comment.User.Id != userId)
        return new RequestResponseDTO() { Code = 401, Message = "Impossível apagar comentário de outro usuário", Success = false };

      _context.Comments.Remove(comment);
      await _context.SaveChangesAsync();
      return new RequestResponseDTO() { Code = 200, Message = "Comentário apagado com sucesso!", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

}
