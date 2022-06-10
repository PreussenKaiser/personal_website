namespace PKaiser.Core.Models;

/// <summary>
/// The class that represents a blog.
/// </summary>
public class Blog
{
    /// <summary>
    /// Gets or sets the blog's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the blog's title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the cover image of the blog.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets when the blog was posted.
    /// </summary>
    public DateTime DatePosted { get; set; }

    /// <summary>
    /// Gets or sets the content of the blog.
    /// </summary>
    public string Content { get; set; }
}
