using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PKaiser.Posts.API.Domain.Entities;
using PKaiser.Posts.API.Options;

namespace PKaiser.Posts.API.Infrastructure.Persistence;

/// <summary>
/// Represents the MongoDB database for posts.
/// </summary>
public sealed class PostsContext
{
	/// <summary>
	/// Initializes <see cref="PostsContext"/> with configuration.
	/// </summary>
	/// <param name="options">Database configuration.</param>
	public PostsContext(IOptions<PostsDatabaseOptions> options)
	{
		PostsDatabaseOptions optionsParsed = options.Value;

		MongoClient client = new(optionsParsed.ConnectionString);
		IMongoDatabase database = client.GetDatabase(optionsParsed.DatabaseName);

		this.Projects = database.GetCollection<Project>(optionsParsed.CollectionName);
	}

	/// <summary>
	/// Gets projects in the database.
	/// </summary>
	public IMongoCollection<Project> Projects { get; }
}
