using PKaiser.Infrastructure.Data;

using PKaiser.Core.Models;
using PKaiser.Core.Services;

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
        blog.DatePosted = DateTime.Now;

        await this.database.Blogs.AddAsync(blog);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Gets all blogs from the local database.
    /// </summary>
    /// <returns>A list of blogs.</returns>
    public async Task<List<Blog>> GetAllBlogsAsync()
        => await Task.Run(() => this.database.Blogs.ToList());

    /// <summary>
    /// Gets a blog from the service.
    /// </summary>
    /// <param name="id">The identifier of the blog to get.</param>
    /// <returns>The found blog.</returns>
    public async Task<Blog> GetBlogAsync(int id)
    {
        Blog blog = await this.database.Blogs.FindAsync(id);

        return blog;
    }

    /// <summary>
    /// Updates a blog in the database.
    /// </summary>
    /// <param name="blog">The blog to update.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task EditBlogAsync(Blog blog)
    {
        if (blog is null)
            return;

        this.database.Blogs.Update(blog);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a blog from the local database.
    /// </summary>
    /// <param name="blog">The blog to remove.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task RemoveBlogAsync(Blog blog)
    {
        this.database.Blogs.Remove(blog);
        await this.database.SaveChangesAsync();
    }
}
