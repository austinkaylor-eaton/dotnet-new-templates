using Microsoft.Extensions.Hosting;

// Create the default Application Host Builder
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// TODO: Add your services to the container.

// Build the Application Host Builder into an Application Host
using IHost host = builder.Build();

// TODO: Add your work here.
await Console.Out.WriteLineAsync("Hello World with C#");

// Run the Application Host
await host.RunAsync();


