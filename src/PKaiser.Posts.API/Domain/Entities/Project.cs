namespace PKaiser.Posts.API.Domain.Entities;

/// <summary>
/// Represents a project I've worked on.
/// </summary>
public sealed class Project : Entity
{
	/// <summary>
	/// Gets the project's title.
	/// </summary>
	public required string Title { get; set; }

	/// <summary>
	/// Gets the project's content.
	/// </summary>
	public required string Content { get; set; }

	/// <summary>
	/// Gets a url to the project.
	/// </summary>
	public required string Url { get; set; }

	/// <summary>
	/// Gets whether the project is featured or not.
	/// </summary>
	public bool IsFeatured { get; init; }
}
