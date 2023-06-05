using PKaiser.Posts.API.Endpoints;
using PKaiser.Posts.API.Extensions;
using PKaiser.Posts.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.ConfigureOptions(builder.Configuration)
	.ConfigureDataAccess();

builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger()
	   .UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
	app.UseMiddleware<KeyMiddleware>();
}

app.UseHttpsRedirection();

app.MapProjects("api/v1/projects");

app.Run();

/// <summary>
/// For detection by integration tests.
/// </summary>
public partial class Program { }
