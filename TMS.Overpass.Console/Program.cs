using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMS.Overpass.Console;
using TMS.Overpass.Console.EtlPipelines;
using TMS.Overpass.Console.Extensions;
using TMS.Overpass.Infrastructure.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services.AddSingleton<App>();
builder.Services.AddEtlPipeline<LinesPipeline>();

builder.Services.AddOpenStreetMapsClient(builder.Configuration);
builder.Services.AddLinesRepository();
builder.Services.AddTmsContext();

IHost host = builder.Build();

IHostApplicationLifetime lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
App app = host.Services.GetRequiredService<App>();

using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(lifetime.ApplicationStopping);

await app.RunAsync(cts.Token);

