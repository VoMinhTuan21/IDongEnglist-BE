using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IDonEnglist.CloudinaryService
{
    public static class CloudinaryServiceRegistration
    {
        public static IServiceCollection ConfigureCloudinaryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddSingleton<ICloudinaryService, CloudinaryService>();

            return services;
        }
    }
}
