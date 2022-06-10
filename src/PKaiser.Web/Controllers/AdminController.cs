using PKaiser.Core.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult ManageBlogs()
        => this.View();

    /// <summary>
    /// Displays the ManageProjects view.
    /// </summary>
    /// <returns>The view fpr managing projects.</returns>
    public IActionResult ManageProjects()
        => this.View();

    /// <summary>
    /// Displays the ManageDocumentation view.
    /// </summary>
    /// <returns>The view for managing documentation.</returns>
    public IActionResult ManageDocumentation()
        => this.View();
}
