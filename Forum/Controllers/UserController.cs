using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
  private readonly TokenService _tokenService;

  public UserController(TokenService tokenService) {
    _tokenService = tokenService;
  }

  [HttpPost]
  public async Task<ActionResult> Create() {
    //Regex PasswordValidations = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
    //string password = "123Aa!";

    //if(PasswordValidations.IsMatch(password))
    //  return Ok("Senha pode ser usada");
    //else
    //  return BadRequest("Senha inválida");

    string token = _tokenService.GenerateToken(new Entities.User { UserName = "gustas", Id = "123321" });
    return Ok(token);
  }
}
