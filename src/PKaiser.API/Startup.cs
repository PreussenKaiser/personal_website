using PKaiser.Core.Services;
using PKaiser.Infrastructure.Data;
using PKaiser.Infrastructure.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        IConfiguration config = BuildConfig();
        string connectionString = config["ConnectionStrings:DefaultConnection"];

        builder.Services.AddDbContext<WebsiteContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IProjectService, ProjectService>();
    }

    /// <summary>
    /// Builds configuration from a config file.
    /// </summary>
    /// <returns>The configuration as defined by the file.</returns>
    private static IConfiguration BuildConfig()
        => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"local.settings.json")
            .Build();
}
