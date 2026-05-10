using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Overpass.Application.Extensions;
using TMS.Overpass.Application.Interfaces.Infrastructure.Http.Clients;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence;
using TMS.Overpass.Infrastructure.Http.Clients;
using TMS.Overpass.Infrastructure.Interfaces;
using TMS.Overpass.Infrastructure.Persistence;
using TMS.Overpass.Infrastructure.Persistence.Context;

namespace TMS.Overpass.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddTmsContext() => services.AddDapperContext<TmsContext>("TmsContext");

        public IServiceCollection AddOpenStreetMapsClient(IConfiguration configuration) 
        {
            string url = configuration.GetRequiredValue<string>("HttpClient:OpenStreetMaps:BaseAddress");
            services.AddHttpClient<IOpenStreetMapsClient,  OpenStreetMapsClient>(x =>
            {
                x.BaseAddress = new Uri(url);
                x.DefaultRequestHeaders.UserAgent.Clear();
                x.DefaultRequestHeaders.UserAgent.ParseAdd("PostmanRuntime/7.54.0");
            })
            .AddStandardResilienceHandler();
            return services;
        }

        internal IServiceCollection AddDapperContext<T>(string serviceKey) where T : class, IDapperContext
        {
            services.AddKeyedScoped<IDapperContext, T>(serviceKey);
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork<T>>(serviceKey);
            return services;
        }
    }
}