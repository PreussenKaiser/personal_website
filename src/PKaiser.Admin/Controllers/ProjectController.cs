using PKaiser.Core.Models;
using PKaiser.Core.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PKaiser.Admin.Controllers;

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
    /// Initializes a new instance of the <see cref="ProjectController"/> class.
    /// </summary>
    /// <param name="projectService">The service to get projects with.</param>
    public ProjectController(IProjectService projectService)
        => this.projectService = projectService;

    /// <summary>
    /// Displays the Add view.
    /// </summary>
    /// <returns>The view for adding projects.</returns>
    [Authorize]
    public IActionResult AddProject()
        => this.View();

    /// <summary>
    /// Adds the posted project then displays the add project view.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>If valid, redirected to ManageProjects.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddProject(Project project)
    {
        if (this.ModelState.IsValid)
        {
            await this.projectService.AddProjectAsync(project);

            return this.RedirectToAction("ManageProjects", "Admin");
        }

        return this.View(project);
    }

    /// <summary>
    /// Displays the ViewProject view.
    /// </summary>
    /// <param name="id">The identifier of the project to display.</param>
    /// <returns>The view to display projects with.</returns>
    public async Task<IActionResult> ViewProject(int id)
    {
        Project project = await this.projectService.GetProjectAsync(id)
            ?? new Project
            {
                Id = 0,
                Title = "There was a problem loading the project",
                Content = string.Empty,
                Url = string.Empty,
                IsFeatured = false
            };

        return this.View(project);
    }

    /// <summary>
    /// Displays the EditProject view.
    /// </summary>
    /// <param name="id">The identifier of the project to edit.</param>
    /// <returns>The view which edits a project.</returns>
    [Authorize]
    public async Task<IActionResult> EditProject(int id)
    {
        Project project = await this.projectService.GetProjectAsync(id);

        return this.View(project);
    }

    /// <summary>
    /// Validates the EditProject form.
    /// </summary>
    /// <param name="project">The porject values to update.</param>
    /// <returns>The view which edits a project.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditProject(Project project)
    {
        if (this.ModelState.IsValid)
        {
            await this.projectService.EditProjectAsync(project);

            return this.RedirectToAction("ManageProjects", "Admin");
        }

        return this.View(project);
    }

    /// <summary>
    /// Deletes a project.
    /// </summary>
    /// <param name="id">The identifier of the project to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    [Authorize]
    public async Task<IActionResult> DeleteProject(int id)
    {
        Project project = await this.projectService.GetProjectAsync(id);

        await this.projectService.DeleteProjectAsync(project);

        return this.RedirectToAction("ManageProjects", "Admin");
    }
}
