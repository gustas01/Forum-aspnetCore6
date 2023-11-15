namespace Forum.IoC;
public static class DependencyInjection {
  public static IServiceCollection AddMyServices(this IServiceCollection services,
      IConfiguration configuration) {


    return services;
  }

  public static IServiceCollection AddInfrastructure(this IServiceCollection services,
      IConfiguration configuration) {


    return services;
  }
}
