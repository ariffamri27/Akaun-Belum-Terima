using IMAS.API.Belanjawan.Client.RefitInterfaces;
using IMAS.Blazor.Belanjawan;
using IMAS.Blazor.Belanjawan.Services.Refit;
using IMAS.Blazor.Belanjawan.Services.Refit.Belanjawan;
using IMAS.Client.Services.Peruntukan;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddRefitClient<IBahagianApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IDanaApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<ILokasiApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IUnitApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IKelulusanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IPelarasanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IPengesahanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IPenyediaanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<ITambahKurangPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<ISemakanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IPenyediaanPendapatanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IHadSilingApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IAgihanPeruntukanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<ILaporanAnggaranPendapatanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<ILaporanBelanjawanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IPertanyaanVotApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IViremenApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

builder.Services.AddRefitClient<IViremenSekatanApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9001"));

// IMPORTANT: Set API base URL here
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:9001/") 
});

await builder.Build().RunAsync();
