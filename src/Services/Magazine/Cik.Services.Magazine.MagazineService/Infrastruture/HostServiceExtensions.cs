using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.Services.Magazine.MagazineService.Infrastruture
{
    public static class HostServiceExtensions
    {
        public static IServiceCollection AddHost(
            this IServiceCollection services, Action<IServiceCollection> additionalDependencies)
        {
            additionalDependencies(services);
            return services;
        }

        /* public static IServiceCollection AddHostCore(this IServiceCollection services)
        {
            
        } */
    }
}