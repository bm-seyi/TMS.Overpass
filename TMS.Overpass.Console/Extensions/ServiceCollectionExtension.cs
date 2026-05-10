using Microsoft.Extensions.DependencyInjection;
using TMS.Overpass.Console.Interfaces;

namespace TMS.Overpass.Console.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection  AddEtlPipeline<T>() where T : class, IEtlPipeline => services.AddTransient<IEtlPipeline, T>();
    }
}