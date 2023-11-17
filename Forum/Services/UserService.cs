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

  public async Task<ActionResult<IdentityResult>> Create(RegisterDTO registerDTO) {
    try {
      User user = _mapper.Map<User>(registerDTO);
      IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
      return result;
    } catch(Exception ex) {
      var errors = new IdentityError() { Code = "500", Description = ex.Message };
      return IdentityResult.Failed(errors);
    }
  }
}
