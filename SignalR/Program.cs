using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SignalR;
using Domain.Shared;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HubConnection connection = new HubConnectionBuilder()
 .WithUrl("https://localhost:7223/zonk", HttpTransportType.WebSockets, o => o.SkipNegotiation = true)
 .WithAutomaticReconnect()
 .Build();

await connection.StartAsync();

builder.Services.AddSingleton((_) => connection);
builder.Services.AddSingleton<IZonkClient, ZonkClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
