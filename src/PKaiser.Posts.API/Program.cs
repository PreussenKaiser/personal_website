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

app.UseMiddleware<KeyMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger()
	   .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProjects("projects");

app.Run();
