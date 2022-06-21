using System.ComponentModel.DataAnnotations;

namespace PKaiser.Core.Models;

/// <summary>
/// Represents a project I've worked on.
/// </summary>
public class Project : Post
{
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
}
