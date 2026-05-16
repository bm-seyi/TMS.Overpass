using Microsoft.Extensions.Configuration;
using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

IResourceBuilder<SqlServerServerResource> devServer = builder.AddSqlServer("DevServer", builder.AddParameter("DevServerPassword", secret: true), 1433)
    .WithLifetime(ContainerLifetime.Session)
    .WithImage("mssql/server", "2025-latest")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithEnvironment("TZ", "Europe/London")
    .WithDataVolume("mssql_data");

IResourceBuilder<SqlServerDatabaseResource> tmsDatabase = devServer.AddDatabase("TMS-Database", "TMS");

builder.AddProject<TMS_Overpass_Console>("TMS")
    .WaitFor(tmsDatabase)
    .WithReference(tmsDatabase, "DefaultConnection");

DistributedApplication distributedApplication = builder.Build();

await distributedApplication.RunAsync();