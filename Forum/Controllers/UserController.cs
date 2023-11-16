using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Forum.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {

  [HttpPost]
  public async Task<ActionResult> Create() {
    Regex PasswordValidations = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
    string password = "123Aa!";

    if(PasswordValidations.IsMatch(password))
      return Ok("Senha pode ser usada");
    else
      return BadRequest("Senha inválida");
  }
}
