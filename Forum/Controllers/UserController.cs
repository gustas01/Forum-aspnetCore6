using Forum.DTOs;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
  private readonly UserService _userService;


  public UserController(UserService userService) {
    _userService = userService;
  }


  //[HttpGet("{userId:Guid}")]
  //public async Task<ActionResult> FindOneByUser(Guid userId) {
  //  var result = await _userService.FindPostsByUser(userId + "");
  //  return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  //}

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

    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }


  [HttpPost("login")]
  public async Task<ActionResult> Login(LoginDTO loginDTO) {
    if(loginDTO == null)
      return BadRequest("Crendenciais inválidas!");

    var result = await _userService.Login(loginDTO);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }

  [Authorize]
  [HttpPut]
  public async Task<ActionResult<string>> Update(UpdateUserDTO updateUserDTO) {
    if(updateUserDTO == null)
      return BadRequest("Dados devem ser fornecidos!");

    string? UserID = HttpContext.User.FindFirst("Jti")?.Value;

    var result = await _userService.Update(UserID, updateUserDTO);
    return new ObjectResult(result?.Value) { StatusCode = result?.Value?.Code };
  }
}
