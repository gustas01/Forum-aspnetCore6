using Forum.Context;
using Microsoft.EntityFrameworkCore;

namespace Forum.IoC;
public static class DependencyInjection {
  public static IServiceCollection AddMyServices(this IServiceCollection services,
      IConfiguration configuration) {


    return services;
  }

  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
      IConfiguration configuration) {

    string stringConection = configuration.GetConnectionString("StringDBConection");
    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(stringConection));

    return services;
  }
}
