using Forum.DTOs;
using System.Security.Claims;

namespace Forum.Services {
  public class TokenService {

    public static string GenerateToken(LoginDTO loginDTO, IList<Claim> roles) {
      return "";
    }
  }
}
