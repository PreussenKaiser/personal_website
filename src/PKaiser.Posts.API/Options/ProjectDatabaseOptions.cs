namespace PKaiser.Posts.API.Options;

/// <summary>
/// Configuration options for the projects database.
/// </summary>
public sealed class PostsDatabaseOptions
{
	/// <summary>
	/// Key for options hierarchy.
	/// </summary>
	public const string PROJECT_DATABASE = "PostsDatabase";

	/// <summary>
	/// Gets the connection string to the database.
	/// </summary>
	public required string ConnectionString { get; init; }

	/// <summary>
	/// Gets the name of the database.
	/// </summary>
	public required string DatabaseName { get; init; }

	/// <summary>
	/// Gets the name of the projects collection.
	/// </summary>
	public required string CollectionName { get; init; }
}
