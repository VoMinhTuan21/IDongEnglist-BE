using IDonEnglist.Application.Services;
using IDonEnglist.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IDonEnglist.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();

            return services;
        }
    }
}
