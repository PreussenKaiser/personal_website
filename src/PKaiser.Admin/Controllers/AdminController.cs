using PKaiser.Core.Models;
using PKaiser.Core.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PKaiser.Admin.Controllers;

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
    /// Initializes a new instance of the <see cref="AdminController"/> class.
    /// </summary>
    /// <param name="projectService">The service to query projects with.</param>
    /// <param name="blogService">The service to query blogs with.</param>
    public AdminController(IProjectService projectService)
    {
        this.projectService = projectService;
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
}
