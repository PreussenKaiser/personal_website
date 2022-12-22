using PKaiser.Web;
using PKaiser.Core.Services;
using PKaiser.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PKaiser.Web.ViewModels;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
	.AddScoped<IProjectService, ProjectService>()
	.AddScoped<IndexViewModel>()
	.AddScoped<ProjectsViewModel>()
	.AddScoped<ProjectDetailViewModel>()
    .AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

await builder
	.Build()
    .RunAsync();
