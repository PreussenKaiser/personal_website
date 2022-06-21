using System.ComponentModel.DataAnnotations;

namespace PKaiser.Core.Models;

/// <summary>
/// Represents a blog.
/// </summary>
public class Blog : Post
{
    /// <summary>
    /// Gets or sets when the blog was posted.
    /// </summary>
    [Required]
    public DateTime DatePosted { get; set; }
}
