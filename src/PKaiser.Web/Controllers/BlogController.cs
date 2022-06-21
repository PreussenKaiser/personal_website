using PKaiser.Core.Models;
using PKaiser.Core.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller that displays blog views.
/// </summary>
public class BlogController : Controller
{
    /// <summary>
    /// The service to get blogs with.
    /// </summary>
    private readonly IBlogService blogService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlogController"/> class.
    /// </summary>
    /// <param name="blogService">The service to get blogs with.</param>
    /// <param name="logger">Logs blog view processes.</param>
    public BlogController(IBlogService blogService)
        => this.blogService = blogService;

    /// <summary>
    /// Displays the blogs index view.
    /// </summary>
    /// <returns>The blogs index view.</returns>
    public async Task<IActionResult> Index()
    {
        IEnumerable<Blog> blogs = await this.blogService.GetAllBlogsAsync();

        return this.View(blogs);
    }

    /// <summary>
    /// Displays the AddBlog view.
    /// </summary>
    /// <returns>The view to add blogs with.</returns>
    [Authorize]
    public IActionResult AddBlog()
        => this.View();

    /// <summary>
    /// Validates the AddBlog form.
    /// </summary>
    /// <param name="blog">The blog to add.</param>
    /// <returns>If valid, redirected to ManageBlogs.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddBlog(Blog blog)
    {
        if (this.ModelState.IsValid)
        {
            await this.blogService.PostBlogAsync(blog);

            return this.RedirectToAction("ManageBlogs", "Admin");
        }

        return this.View(blog);
    }

    /// <summary>
    /// Displays the ViewBlog view.
    /// </summary>
    /// <param name="id">The blog to view.</param>
    /// <returns>The view which displays a blog.</returns>
    public async Task<IActionResult> ViewBlog(int id)
    {
        Blog blog = await this.blogService.GetBlogAsync(id)
            ?? new Blog
            {
                Id = 0,
                Title = "There was a problem loading the blog",
                DatePosted = DateTime.Now,
                Content = string.Empty
            };

        return this.View(blog);
    }

    /// <summary>
    /// Displays the EditBlog view.
    /// </summary>
    /// <param name="blogId">The identifier of the blog to edit.</param>
    /// <returns>The view to edit blogs with.</returns>
    [Authorize]
    public async Task<IActionResult> EditBlog(int blogId)
    {
        Blog blog = await this.blogService.GetBlogAsync(blogId);

        return this.View(blog);
    }

    /// <summary>
    /// Validates the EditBlog form.
    /// </summary>
    /// <param name="blog">The blog to edit.</param>
    /// <returns>If valid, redirected to ManageBlogs.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditBlog(Blog blog)
    {
        if (this.ModelState.IsValid)
        {
            await this.blogService.EditBlogAsync(blog);

            return this.RedirectToAction("ManageBlogs", "Admin");
        }

        return this.View(blog);
    }

    /// <summary>
    /// Removes a blog.
    /// </summary>
    /// <param name="id">The identifier of the blog to remove.</param>
    /// <returns>Redirected to ManageBlogs.</returns>
    [Authorize]
    public async Task<IActionResult> RemoveBlog(int id)
    {
        Blog blog = await this.blogService.GetBlogAsync(id);

        await this.blogService.RemoveBlogAsync(blog);

        return this.RedirectToAction("ManageBlogs", "Admin");
    }
}
