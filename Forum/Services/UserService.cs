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
      return new RequestResponseDTO() { Code = 200, Message = $"Usuário {registerDTO.UserName} criado com sucesso", Success = true };
    } catch(Exception ex) {
      return new RequestResponseDTO() { Code = 500, Message = ex.Message, Success = false };
    }
  }

  public async Task<ActionResult<RequestResponseDTO>> Login(LoginDTO loginDTO) {
    var user = await _userManager.FindByEmailAsync(loginDTO.Email);

    if(user == null)
      return new RequestResponseDTO() { Code = 400, Message = "Credenciais inválidas", Success = false };


    return new RequestResponseDTO() { Code = 200, Message = _tokenService.GenerateToken(user), Success = true };
  }
}
