using BlazorAppStayly;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración de HttpClient para API
builder.Services.AddHttpClient("ApiStayly", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7054/");
    //httpClient.BaseAddress = new Uri("https://apiprueba.runasp.net");
});
// HttpClient para recursos locales
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Configuración de MudBlazor (versión 6.9.0+ para .NET 9)
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.PopoverOptions.ThrowOnDuplicateProvider = false; // Elimina advertencia de popover duplicado
});

// Configuración de Blazored
builder.Services.AddBlazoredLocalStorage();

// Autenticación
builder.Services.AddAuthorizationCore();

// Usuario Actual
builder.Services.AddScoped<UserStateService>();

await builder.Build().RunAsync();
