using PKaiser.Posts.API.Domain.Repositories;
using PKaiser.Posts.API.Infrastructure.Repositories;
using PKaiser.Posts.API.Options;

namespace PKaiser.Posts.API.Extensions;

/// <summary>
/// Extensions methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds application configuration to DI.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	/// <param name="configuration">Application configuration.</param>
	/// <returns>DI with application configuration.</returns>
	public static IServiceCollection ConfigureOptions(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<ProjectDatabaseOptions>(configuration.GetRequiredSection(ProjectDatabaseOptions.PROJECT_DATABASE));
		services.Configure<AuthenticationOptions>(configuration.GetRequiredSection(AuthenticationOptions.AUTHENTICATION));

		return services;
	}

	/// <summary>
	/// Adds data-access to DI.
	/// </summary>
	/// <param name="services">Application DI.</param>
	/// <returns>DI with data-access.</returns>
	public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
	{
		services.AddSingleton<IProjectRepository, MongoProjectRepository>();

		return services;
	}
}
