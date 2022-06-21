using System.ComponentModel.DataAnnotations;

namespace PKaiser.Core.Models;

/// <summary>
/// Represents a post.
/// </summary>
public abstract class Post
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
    /// Gets or sets the post's content.
    /// </summary>
    [Required]
    [DataType(DataType.Html)]
    public string Content { get; set; }
}
