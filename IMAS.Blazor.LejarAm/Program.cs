using IMAS.API.LejarAm.Shared.Infrastructure.Refit;
using IMAS.Blazor.LejarAm;
using IMAS.Blazor.LejarAm.Services.Refit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

// Common Refit settings with case-insensitive JSON
var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // optional, helps align with typical WebAPI
        })
};



// Register Refit clients with settings
builder.Services.AddRefitClient<IAuditTrailApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9003"));

builder.Services.AddRefitClient<IAuditTrailFilterApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9003"));

builder.Services.AddRefitClient<IJurnalApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9003"));

builder.Services.AddRefitClient<IPenyelenggaraanLejarApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9003"));

// Optional plain HttpClient if needed elsewhere
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:9003/")
});

await builder.Build().RunAsync();
