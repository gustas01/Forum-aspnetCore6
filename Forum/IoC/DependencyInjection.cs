using AutoMapper;
using Forum.Context;
using Forum.Entities;
using Forum.Mapper;
using Forum.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Forum.IoC;
public static class DependencyInjection {
  public static IServiceCollection AddMyServices(this IServiceCollection services,
      IConfiguration configuration) {
    //configurações do automapper
    var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MapperProfile()); });

    services.AddSingleton(mappingConfig.CreateMapper());
    services.AddSingleton<TokenService>();
    services.AddTransient<UserService>();
    services.AddTransient<CommentService>();
    services.AddTransient<PostService>();
    services.AddTransient<CommunityService>();

    return services;
  }




  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
      IConfiguration configuration) {
    string key = configuration["Jwt:Key"];

    string stringConection = configuration.GetConnectionString("StringDBConection");
    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(stringConection));

    services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

    services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

  .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
    ClockSkew = TimeSpan.Zero,
  });



    return services;
  }
}
