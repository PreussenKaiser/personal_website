using PKaiser.Posts.API.Domain.Repositories;
using PKaiser.Posts.API.Infrastructure.Persistence;
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
		services
			.Configure<PostsDatabaseOptions>(configuration.GetRequiredSection(PostsDatabaseOptions.PROJECT_DATABASE))
			.Configure<AuthenticationOptions>(configuration.GetRequiredSection(AuthenticationOptions.AUTHENTICATION));

		return services;
	}

	/// <summary>
	/// Adds data-access to DI.
	/// </summary>
	/// <param name="services">Application DI.</param>
	/// <returns>DI with data-access.</returns>
	public static IServiceCollection ConfigureDataAccess(this IServiceCollection services)
	{
		services
			.AddSingleton<PostsContext>()
			.AddSingleton<IProjectRepository, MongoProjectRepository>();

		return services;
	}
}
