using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// The interface that implements blog query methods.
/// </summary>
public interface IBlogService
{
    /// <summary>
    /// Creates a new blog in the service asynchronously.
    /// </summary>
    /// <param name="blog">The blog to create.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task PostBlogAsync(Blog blog);

    /// <summary>
    /// Gets all blogs from the service asynchronously.
    /// </summary>
    /// <returns>A list of blogs.</returns>
    public Task<List<Blog>> GetAllBlogsAsync();

    /// <summary>
    /// Deletes a blog from the service asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the blog to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveBlogAsync(int id);
}
