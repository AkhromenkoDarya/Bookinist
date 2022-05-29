using Bookinist.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.Services.Registration
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<ISaleService, SaleService>()
            .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
