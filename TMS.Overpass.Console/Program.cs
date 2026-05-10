using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TMS.Overpass.Application.EtlPipelines;
using TMS.Overpass.Console.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services.AddEtlPipeline<LinesPipeline>();

IHost host = builder.Build();
