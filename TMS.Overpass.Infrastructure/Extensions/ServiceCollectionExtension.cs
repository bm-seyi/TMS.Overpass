using Microsoft.Extensions.DependencyInjection;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence;
using TMS.Overpass.Infrastructure.Interfaces;
using TMS.Overpass.Infrastructure.Persistence;
using TMS.Overpass.Infrastructure.Persistence.Context;

namespace TMS.Overpass.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddDapperContext<T>(string serviceKey) where T : class, IDapperContext
        {
            services.AddKeyedScoped<IDapperContext, T>(serviceKey);
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork<T>>(serviceKey);
            return services;
        }

        public  IServiceCollection AddTmsContext() => services.AddDapperContext<TmsContext>("TmsContext");
    }
}