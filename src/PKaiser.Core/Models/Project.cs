using System.ComponentModel.DataAnnotations;

namespace PKaiser.Core.Models;

/// <summary>
/// Represents a project I've worked on.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or initializes the post's unique identifier.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the post's title.
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets a brief summary of the post.
    /// </summary>
    [Required]
    public string Details { get; set; }

    /// <summary>
    /// Gets or sets the project's url.
    /// </summary>
    [Required]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the post's content.
    /// </summary>
    [Required]
    [DataType(DataType.Html)]
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets whether the project is featured or not.
    /// </summary>
    [Required]
    public bool IsFeatured { get; set; }
}
