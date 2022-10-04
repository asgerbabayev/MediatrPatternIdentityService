using Code.Application.Common.Interfaces;
using Code.Infrastructure.Identity;
using Code.Infrastructure.Persistance;
using Code.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Code.Infrastructure;
public static class ConfugurationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlServer(configuration.GetConnectionString("default"),
            builderOpt => builderOpt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddTransient<IDateTime, DateTimeService>();
        //services.AddTransient<IIdentityService,IdentityService>();
        services.AddIdentityServer().AddApiAuthorization<AppUser, AppDbContext>(opt =>
        {
        });
        return services;
    }
}
