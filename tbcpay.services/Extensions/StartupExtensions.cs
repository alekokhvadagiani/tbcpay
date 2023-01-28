using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using tbcpay.services.Middlewares;
using tbcpay.services.ProviderService;
using tbcpay.services.ProviderService.Abstracts;
using tbcpay.services.ServiceFilters;


namespace tbcpay.services.Extensions
{
    public static class StartupExtensions
    {
        public static void UseMiddlewares(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ActionRedirectionMiddleware>();
            builder.UseMiddleware<ExceptionMiddleware>();
        }

        public static void AddCustomFilters(this IServiceCollection services)
        {
            services.AddScoped<ModelStateFilter>();
        }

        public static void AddInterfaces(this IServiceCollection services)
        {
            services.AddScoped<ICheck, Check>();
            services.AddScoped<IPay, Pay>();
        }
    }
}