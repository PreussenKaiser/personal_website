using PKaiser.Core.Services;

using Microsoft.AspNetCore.Mvc;
using PKaiser.Core.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller that renders project views.
/// </summary>
public class ProjectController : Controller
{
    /// <summary>
    /// The service to get projects with.
    /// </summary>
    private readonly IProjectService projectService;

    /// <summary>
    /// Logs project view processes.
    /// </summary>
    private readonly ILogger<ProjectController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectController"/> class.
    /// </summary>
    /// <param name="projectService">The service to get projects with.</param>
    /// <param name="logger">Logs project view processes.</param>
    public ProjectController(IProjectService projectService,
                             ILogger<ProjectController> logger)
    {
        this.projectService = projectService;
        this.logger = logger;
    }

    /// <summary>
    /// Displays the Index view.
    /// </summary>
    /// <returns>The main project view.</returns>
    public async Task<IActionResult> Index()
    {
        IEnumerable<Project> projects = await this.projectService.GetAllProjectsAsync();

        return this.View(projects);
    }

    /// <summary>
    /// Displays the Add view.
    /// </summary>
    /// <returns>The view for adding projects.</returns>
    [Authorize]
    public IActionResult Add()
        => this.View();

    /// <summary>
    /// Adds the posted project then displays the add project view.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>The view for adding projects.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(Project project)
    {
        if (this.ModelState.IsValid)
        {
            await this.projectService.AddProjectAsync(project);
        }

        return this.View(project);
    }

    /// <summary>
    /// Displays the ViewProject view.
    /// </summary>
    /// <param name="projectId">The identifier of the project to display.</param>
    /// <returns>The view to display projects with.</returns>
    public async Task<IActionResult> ViewProject(int projectId)
    {
        Project project = await this.projectService.GetProjectAsync(projectId)
            ?? new Project
            {
                Id = 0,
                Title = "Error finding project",
                Description = string.Empty,
                Url = string.Empty,
                IsFeatured = false
            };

        return this.View(project);
    }
}
