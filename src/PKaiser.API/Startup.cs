using PKaiser.Core.Services;
using PKaiser.Infrastructure.Data;
using PKaiser.Infrastructure.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(PKaiser.API.Startup))]
namespace PKaiser.API;

/// <summary>
/// The main entry-point of the API.
/// </summary>
public class Startup : FunctionsStartup
{
    /// <summary>
    /// Builds services for the API.
    /// </summary>
    /// <param name="builder">The container to add services to.</param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // TODO: Change this!
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-PKaiser-C3BF5B2A-1E8F-4EED-8796-37909CA30DED;Trusted_Connection=True;MultipleActiveResultSets=true";

        builder.Services.AddDbContext<WebsiteContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IProjectService, ProjectService>();
    }
}
