using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WotBlitzStatisticsPro.WebUi;
using WotBlitzStatisticsPro.WebUi.Services;
using MediatR;
using System.Reflection;
using WotBlitzStatisticsPro.WebUi.Messages;
using System.Globalization;
using WotBlitzStatisticsPro.Application;
using WotBlitzStatisticsPro.Application.Messages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

ApplicationInstaller.ConfigureServices(builder.Services);

// Register all MediatR handlers from all assemblies
var mediatRAssembliesToRegister = ApplicationInstaller.GetAllMediatRAssemblies();
mediatRAssembliesToRegister.Add(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(mediatRAssembliesToRegister.ToArray());

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
