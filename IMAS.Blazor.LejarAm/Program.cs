using IMAS.Blazor.LejarAm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using IMAS.API.LejarAm.Shared.Infrastructure.Refit;
using Refit;
using IMAS.Blazor.LejarAm.Services.Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddRefitClient<IAuditTrailApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9002"));

builder.Services.AddRefitClient<IAuditTrailFilterApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9002"));

builder.Services.AddRefitClient<IJurnalApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9002"));

builder.Services.AddRefitClient<IPenyelenggaraanLejarApi>()
	.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9002"));

// IMPORTANT: Set API base URL here
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:9002/") 
});

await builder.Build().RunAsync();
