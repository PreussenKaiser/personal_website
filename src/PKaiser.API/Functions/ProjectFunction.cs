using PKaiser.Core.Services;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace PKaiser.API.Functions;

/// <summary>
/// Handles requests for projects.
/// </summary>
public class ProjectFunction
{
    /// <summary>
    /// The service to get projects with.
    /// </summary>
    private readonly IProjectService projectService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectFunction"/> class.
    /// </summary>
    /// <param name="projectService">The service to get projects with.</param>
    public ProjectFunction(IProjectService projectService)
        => this.projectService = projectService;

    /// <summary>
    /// Gets all projects from the API.
    /// </summary>
    /// <param name="req">The request to handle.</param>
    /// <param name="logger">Logs response processes.</param>
    /// <returns>Projects from the service.</returns>
    [FunctionName("projects")]
    public async Task<IActionResult> GetAllProjects(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger logger)
    {
        logger.LogInformation("Processed request for projects.");

        var projects = await projectService.GetAllProjectsAsync();
        string json = JsonConvert.SerializeObject(projects);

        return new OkObjectResult(json);
    }

    /// <summary>
    /// Gets a project from the API.
    /// </summary>
    /// <param name="req">Th request to handle.</param>
    /// <param name="logger">Logs response processes.</param>
    /// <returns>The found project.</returns>
    [FunctionName("project")]
    public async Task<IActionResult> GetProject(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger logger)
    {
        logger.LogInformation("Processed request for a project.");

        int projectId = int.Parse(req.Query["id"]);

        var project = await this.projectService.GetProjectAsync(projectId);
        string json = JsonConvert.SerializeObject(project);

        return new OkObjectResult(json);
    }
}
