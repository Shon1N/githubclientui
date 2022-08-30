using GitHubClient.Ui;
using GitHubClient.Ui.Services;
using GitHubClient.Ui.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiUrl = builder.Configuration.GetSection("ApiUrl:DefaultApi").Value;//wwwwroot
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddScoped<IRequestTokenService, RequestTokenService>();
builder.Services.AddScoped<IGitHubService, GitHubService>();
await builder.Build().RunAsync();
