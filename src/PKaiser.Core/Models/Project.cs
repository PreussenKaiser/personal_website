namespace PKaiser.Core.Models;

/// <summary>
/// The class that represents a project I've worked on.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the project's unique identifier.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets whether the project is featured or not.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Gets or sets the project's title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the project's description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the project's url.
    /// </summary>
    public string Url { get; set; }
}
