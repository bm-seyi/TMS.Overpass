using Aspire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects;


IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.AddProject<TMS_Overpass_Console>("TMS");

DistributedApplication distributedApplication = builder.Build();

await distributedApplication.RunAsync();