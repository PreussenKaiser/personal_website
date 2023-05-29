namespace PKaiser.Posts.API.Options;

/// <summary>
/// Configurtion options for the projects database.
/// </summary>
public sealed class ProjectDatabaseOptions
{
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
