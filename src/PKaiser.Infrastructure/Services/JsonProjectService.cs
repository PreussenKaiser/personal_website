using PKaiser.Infrastructure.Extensions;

using PKaiser.Core.Models;
using PKaiser.Core.Services;

using System.Net.Http.Json;

namespace PKaiser.Infrastructure.Services;

/// <summary>
/// Queries projects using a local JSON file.
/// </summary>
public class JsonProjectService : IProjectService
{
    /// <summary>
    /// The path to the JSON document.
    /// </summary>
    private const string FILENAME = "data/projects.json";

    /// <summary>
    /// The http client to read and write JSON with.
    /// </summary>
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonProjectService"/> class.
    /// </summary>
    /// <param name="client">The http client to read and write JSON with.</param>
    public JsonProjectService(HttpClient client)
        => this.client = client;

    /// <summary>
    /// Adds a project to the JSON document.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddProjectAsync(Project project)
        => await this.client.PostAsJsonAsync(FILENAME, project);

    /// <summary>
    /// Gets all projects from the JSON document.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var projects = await this.client.GetFromJsonAsync<Project[]>(FILENAME);

        return projects;
    }

    /// <summary>
    /// Gets all featured projects from the JSON document.
    /// </summary>
    /// <returns>Featured projects from the document.</returns>
    public async Task<IEnumerable<Project>> GetAllFeaturedProjectsAsync()
    {
        IEnumerable<Project> projects = await this.GetAllProjectsAsync();

        projects = projects.Where(p => p.IsFeatured);

        return projects;
    }

    /// <summary>
    /// Gets a project from the JSON document.
    /// </summary>
    /// <param name="projectId">The identifier of the project to get.</param>
    /// <returns>The found project.</returns>
    public async Task<Project> GetProjectAsync(int projectId)
    {
        var projects = await this.GetAllProjectsAsync();
        Project foundProject = projects.First(p => p.Id == projectId);

        return foundProject;
    }

    /// <summary>
    /// Updates a project in the JSON document.
    /// </summary>
    /// <param name="project">The project to update.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task EditProjectAsync(Project project)
        => await this.client.PutAsJsonAsync(FILENAME, project);

    /// <summary>
    /// Deletes a project from the JSON document.
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public async Task DeleteProjectAsync(Project project)
        => await this.client.DeleteAsJsonAsync(FILENAME, project);
}
