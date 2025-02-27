using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// TODO: Add your services to the container.

using IHost host = builder.Build();
await host.RunAsync();
