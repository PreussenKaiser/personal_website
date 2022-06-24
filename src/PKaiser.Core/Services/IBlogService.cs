using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// Implements blog query methods.
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
    /// Gets a blog from the service.
    /// </summary>
    /// <param name="id">The identifier of the blog to get.</param>
    /// <returns>The found blog.</returns>
    public Task<Blog> GetBlogAsync(int id);

    /// <summary>
    /// Updates a blog in the service.
    /// </summary>
    /// <param name="blog">The blog to edit.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task EditBlogAsync(Blog blog);

    /// <summary>
    /// Deletes a blog from the service asynchronously.
    /// </summary>
    /// <param name="blog">The blog to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveBlogAsync(Blog blog);
}
