using System.ComponentModel.DataAnnotations;

namespace PKaiser.Core.Models;

/// <summary>
/// The class that represents a project I've worked on.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the project's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the project's title.
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the project's url.
    /// </summary>
    [Required]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets whether the project is featured or not.
    /// </summary>
    [Required]
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Gets or sets the project's description.
    /// </summary>
    [Required]
    [DataType(DataType.Html)]
    public string Description { get; set; }
}
