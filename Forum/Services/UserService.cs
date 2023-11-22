using AutoMapper;
using Forum.DTOs;
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Services;
public class UserService {
  private readonly UserManager<User> _userManager;
  private readonly TokenService _tokenService;
  private readonly IMapper _mapper;

  public UserService(UserManager<User> userManager, TokenService tokenService, IMapper mapper) {
    _userManager = userManager;
    _tokenService = tokenService;
    _mapper = mapper;
  }

  public async Task<ActionResult<RequestResponseDTO>> Create(RegisterDTO registerDTO) {
    try {
      User user = _mapper.Map<User>(registerDTO);
      IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
      if(!result.Succeeded) return new RequestResponseDTO() { Code = 400, Message = $"{result.Errors.First().Description}", Success = false };
      return new RequestResponseDTO() { Code = 200, Message = $"Usuário {registerDTO.UserName} criado com sucesso", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

  public async Task<ActionResult<RequestResponseDTO>> Login(LoginDTO loginDTO) {
    try {
      var user = await _userManager.FindByEmailAsync(loginDTO.Email);


      if(user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        return new RequestResponseDTO() { Code = 400, Message = "Credenciais inválidas", Success = false };

      return new RequestResponseDTO() { Code = 200, Message = new { token = _tokenService.GenerateToken(user) }, Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

  public async Task<ActionResult<RequestResponseDTO>> Update(string? UserId, UpdateUserDTO updateUserDTO) {

    User user = await _userManager.FindByIdAsync(UserId);

    _mapper.Map(updateUserDTO, user);

    if(updateUserDTO.Password != null) {
      await _userManager.RemovePasswordAsync(user);
      await _userManager.AddPasswordAsync(user, updateUserDTO.Password);
    }

    await _userManager.UpdateAsync(user);

    return new RequestResponseDTO() { Code = 200, Message = "Usuário atualizado com sucesso", Success = true };
  }
}
