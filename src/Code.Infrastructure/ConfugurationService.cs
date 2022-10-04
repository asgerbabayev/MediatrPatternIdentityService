namespace Code.Infrastructure;
public static class ConfugurationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlServer(configuration.GetConnectionString("default"),
            builderOpt => builderOpt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(factory => factory.GetRequiredService<AppDbContext>());
        services.AddScoped<AppDbContextInitializer>();
        services.AddDefaultIdentity<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddIdentityServer().AddApiAuthorization<AppUser, AppDbContext>();

        //services.AddTransient<IIdentityService,IdentityService>();

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        return services;
    }
}
