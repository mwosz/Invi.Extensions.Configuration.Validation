using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Invi.Extensions.Configuration.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseConfigurationValidation(this IServiceCollection services)
        {
            services.AddSingleton<ValidationStartupFilter>();
            using var scope = services.BuildServiceProvider().CreateScope();

            return services;
        }

        public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection services, IConfiguration config)
            where T : class, IValidation<T>, new()
        {
            services.Configure<T>(config);
            services.AddSingleton<IValidation>(r => r.GetRequiredService<IOptions<T>>().Value);
            return services;
        }        
    }
}