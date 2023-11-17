using Forum.DTOs;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
  private readonly UserService _userService;


  public UserController(UserService userService) {
    _userService = userService;
  }

  [HttpPost("register")]
  public async Task<ActionResult> Create(RegisterDTO registerDTO) {
    //Regex PasswordValidations = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
    //string password = "123Aa!";

    //if(PasswordValidations.IsMatch(password))
    //  return Ok("Senha pode ser usada");
    //else
    //  return BadRequest("Senha inválida");
    if(registerDTO == null)
      return BadRequest("Usuário deve ser informado");

    var result = await _userService.Create(registerDTO);

    return result.Value.Succeeded ? Ok($"Usuário {registerDTO.UserName} criado com sucesso") : BadRequest(result?.Value?.Errors);
  }
}
