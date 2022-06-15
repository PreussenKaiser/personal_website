using PKaiser.Core.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PKaiser.Core.Models;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller that renders admin views.
/// </summary>
[Authorize]
public class AdminController : Controller
{
    /// <summary>
    /// The service to query projects with.
    /// </summary>
    private readonly IProjectService projectService;

    /// <summary>
    /// The service to query blogs with.
    /// </summary>
    private readonly IBlogService blogService;

    /// <summary>
    /// Logs administration processes.
    /// </summary>
    private readonly ILogger<AdminController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminController"/> class.
    /// </summary>
    /// <param name="projectService">The service to query projects with.</param>
    /// <param name="blogService">The service to query blogs with.</param>
    /// <param name="logger">Logs administration processes.</param>
    public AdminController(IProjectService projectService,
                           IBlogService blogService,
                           ILogger<AdminController> logger)
    {
        this.projectService = projectService;
        this.blogService = blogService;
        this.logger = logger;
    }

    /// <summary>
    /// Displays the ManageBlogs view.
    /// </summary>
    /// <returns>The view for managing blogs.</returns>
    public async Task<IActionResult> ManageBlogs()
    {
        IEnumerable<Blog> blogs = await this.blogService.GetAllBlogsAsync();

        return this.View(blogs);
    }

    /// <summary>
    /// Displays the ManageProjects view.
    /// </summary>
    /// <returns>The view for managing projects.</returns>
    public async Task<IActionResult> ManageProjects()
    {
        IEnumerable<Project> projects = await this.projectService.GetAllProjectsAsync();

        return this.View(projects);
    }

    /// <summary>
    /// Displays the ManageDocumentation view.
    /// </summary>
    /// <returns>The view for managing documentation.</returns>
    public IActionResult ManageDocumentation()
        => this.View();
}
