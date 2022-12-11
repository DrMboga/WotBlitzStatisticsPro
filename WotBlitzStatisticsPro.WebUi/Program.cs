using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WotBlitzStatisticsPro.WebUi;
using MediatR;
using WotBlitzStatisticsPro.WebUi.Messages;
using System.Globalization;
using WotBlitzStatisticsPro.Application;
using WotBlitzStatisticsPro.WebUi.Model;
using WotBlitzStatisticsPro;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// AppSettings
var wgApiConfig = new WargamingApiSettings();
builder.Configuration.GetSection("WargamingApi").Bind(wgApiConfig);
builder.Services.AddSingleton<IWargamingApiSettings>(wgApiConfig);

ApplicationInstaller.ConfigureServices(builder.Services, wgApiConfig.UseMockData);

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var host = builder.Build();
// Reading the theme and locale from local storage
var mediator = host.Services.GetRequiredService<IMediator>();
var localState = await mediator.Send(new LocalStateRequest());

// Applying theme
await mediator.Publish(new SwitchThemeNotification(localState.IsDarkTheme));
// Applying culture
var culture = new CultureInfo(localState.Locale);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
