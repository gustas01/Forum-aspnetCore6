using AutoMapper;
using Forum.Context;
using Forum.DTOs;
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services;
public class CommunityService {
  private readonly AppDbContext _context;
  private readonly UserManager<User> _userManager;
  private readonly IMapper _mapper;

  public CommunityService(AppDbContext context, UserManager<User> userManager, IMapper mapper) {
    _context = context;
    _mapper = mapper;
    _userManager = userManager;
  }


  public async Task<ActionResult<RequestResponseDTO>> FindOne(Guid communityId) {
    try {
      Community? community = await _context.Communities
        .Include(c => c.UserMods)
        .Include(c => c.UserMembers)
        .Include(c => c.Posts)
        .SingleOrDefaultAsync(p => p.Id == communityId);

      if(community == null) return new RequestResponseDTO() { Code = 404, Message = "Comunidade apagada ou inexistente!", Success = false };

      return new RequestResponseDTO() { Code = 200, Message = community, Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }


  public async Task<ActionResult<RequestResponseDTO>> Create(CreateCommunityDTO createCommunityDTO, string userId) {
    try {
      if(createCommunityDTO.Subject == String.Empty || createCommunityDTO.Subject == null || createCommunityDTO.Description == null || createCommunityDTO.Description == String.Empty)
        return new RequestResponseDTO() { Code = 400, Message = "Post vazio inválido!", Success = false };

      Community? community = _mapper.Map<Community>(createCommunityDTO);
      User user = await _userManager.FindByIdAsync(userId);
      community.UserMods.Add(user);
      community.UserMembers.Add(user);
      user.CommunitiesAsMod.Add(community);
      user.CommunitiesAsMember.Add(community);

      _context.Communities.Add(community);
      await _context.SaveChangesAsync();

      return new RequestResponseDTO() { Code = 201, Message = "Comunidade criada", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }


  public async Task<ActionResult<RequestResponseDTO>> Update(UpdateCommunityDTO updateCommunityDTO, string? userId, Guid? communityId) {
    try {
      if(updateCommunityDTO.Subject == String.Empty || updateCommunityDTO.Subject == null || updateCommunityDTO.Description == null || updateCommunityDTO.Description == String.Empty)
        return new RequestResponseDTO() { Code = 400, Message = "Comunidade vazia inválido!", Success = false };

      Community? community = await _context.Communities.Include(c => c.UserMods).SingleOrDefaultAsync(c => c.Id == communityId);

      if(community == null) return new RequestResponseDTO() { Code = 404, Message = "Comunidade apagada ou inexistente!", Success = false };

      //verificando se um usuário diferente está tentando modificar comentário do atual
      if(community.UserMods.Find(u => u.Id == userId) == null)
        return new RequestResponseDTO() { Code = 403, Message = "Impossível alterar Comunidade sem ser um moderador", Success = false };

      _mapper.Map(updateCommunityDTO, community);
      _context.Entry(community).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return new RequestResponseDTO() { Code = 200, Message = "Comunidade atualizada com sucesso!", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }
}
