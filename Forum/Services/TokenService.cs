using Forum.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Services {
  public class TokenService {
    static IConfiguration _configuration;
    public TokenService(IConfiguration configuration) {
      _configuration = configuration;
    }

    public string GenerateToken(User user) {
      var tokenClaims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.Jti, user.Id),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expirationTime = DateTime.UtcNow.AddHours(Double.Parse(_configuration["TokenConfiguration:ExpiredHours"]));


      var JwtToken = new JwtSecurityToken(
        issuer: _configuration["TokenConfiguration:Issuer"],
        audience: _configuration["TokenConfiguration:Audience"],
        claims: tokenClaims,
        expires: expirationTime,
        signingCredentials: credentials
        );

      return new JwtSecurityTokenHandler().WriteToken(JwtToken);
    }
  }
}
