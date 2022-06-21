using PKaiser.Web.ViewModels;

using PKaiser.Core.Models;
using PKaiser.Core.Services;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller which displays home views.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// The service to get projects with.
    /// </summary>
    private readonly IProjectService projectService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="projectService">The service to get projects with.</param>
    /// <param name="logger">The logger for home views.</param>
    public HomeController(IProjectService projectService)
        => this.projectService = projectService;

    /// <summary>
    /// Renders the index view.
    /// </summary>
    /// <returns>The index view.</returns>
    public async Task<IActionResult> Index()
    {
        IEnumerable<Project> projects = await this.projectService.GetAllFeaturedProjectsAsync();

        return this.View(projects);
    }

    /// <summary>
    /// Renders the error view.
    /// </summary>
    /// <returns>The error view.</returns>
    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
        => this.View(new ErrorViewModel 
                    {
                        RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
                    });
}