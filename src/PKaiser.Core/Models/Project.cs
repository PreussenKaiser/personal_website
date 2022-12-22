namespace PKaiser.Core.Models;

/// <summary>
/// Represents a project I've worked on.
/// </summary>
/// <param name="Id">The  project's unique identifier.</param>
/// <param name="Title">The project's title.</param>
/// <param name="Details">A quick summary of the project.</param>
/// <param name="Content">The project articles content.</param>
/// <param name="Url">A url to the project.</param>
/// <param name="IsFeatured">Whether the project is featured or not.</param>
public sealed record Project(
	int Id,
	string Title,
	string Details,
	string Content,
	string Url,
	bool IsFeatured) : IModel<int>;
