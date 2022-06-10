using PKaiser.Core.Models;
using PKaiser.Core.Services;
using PKaiser.Infrastructure.Data;

namespace PKaiser.Infrastructure.Services;

/// <summary>
/// The service that gets blogs from the local database.
/// </summary>
public class BlogService : IBlogService
{
    /// <summary>
    /// The database to query blogs with.
    /// </summary>
    private readonly WebsiteContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlogService"/> class.
    /// </summary>
    /// <param name="database">The database to query blogs with.</param>
    public BlogService(WebsiteContext database)
        => this.database = database;

    /// <summary>
    /// Creates a blog in the database.
    /// </summary>
    /// <param name="blog">The blog to create.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task PostBlogAsync(Blog blog)
    {
        this.database.Blogs.Add(blog);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Gets all blogs from the local database.
    /// </summary>
    /// <returns>A list of blogs.</returns>
    public async Task<List<Blog>> GetAllBlogsAsync()
        => await Task.Run(() => this.database.Blogs.ToList());

    /// <summary>
    /// Deletes a blog from the local database.
    /// </summary>
    /// <param name="id">The identifier of the blog to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task RemoveBlogAsync(int id)
    {
        throw new NotImplementedException();
    }
}
