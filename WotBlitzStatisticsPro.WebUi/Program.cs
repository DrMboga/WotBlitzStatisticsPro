using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WotBlitzStatisticsPro.WebUi;
using WotBlitzStatisticsPro.WebUi.Services;
using MediatR;
using System.Reflection;
using WotBlitzStatisticsPro.WebUi.Messages;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// TODO: Find out the way to scan the other assemblies
// https://github.com/jbogard/MediatR/wiki#aspnet-core-or-net-core-in-general
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<LocalStateService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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
