namespace PKaiser.Posts.API.Domain.Entities;

/// <summary>
/// Represents a project I've worked on.
/// </summary>
public sealed class Project : Entity
{
	/// <summary>
	/// Initializes the <see cref="Project"/> entity.
	/// </summary>
	public Project() : base()
	{
	}

	/// <summary>
	/// Gets the project's title.
	/// </summary>
	public required string Title { get; init; }

	/// <summary>
	/// Gets a short summary of the project.
	/// </summary>
	public required string Details { get; init; }

	/// <summary>
	/// Gets the project's content.
	/// </summary>
	public required string Content { get; init; }

	/// <summary>
	/// Gets a url to the project.
	/// </summary>
	public required string Url { get; init; }

	/// <summary>
	/// Gets whether the project is featured or not.
	/// </summary>
	public bool IsFeatured { get; init; }
}
