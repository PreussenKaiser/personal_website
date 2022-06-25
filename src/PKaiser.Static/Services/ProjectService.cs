using PKaiser.Core.Models;
using PKaiser.Core.Services;
using System.Net.Http.Json;

namespace PKaiser.Static.Services;

/// <summary>
/// Queries projects using a local JSON file.
/// </summary>
/// <remarks>
/// I would put this in the Infrastructure project, but this can only work if it's part of the static page.
/// </remarks>
public class ProjectService : IProjectService
{
    /// <summary>
    /// Path to the JSON file.
    /// </summary>
    private const string FILENAME = "data/projects.json";

    /// <summary>
    /// The client to read JSON with.
    /// </summary>
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectService"/> class.
    /// </summary>
    /// <param name="client">The client to read JSON with.</param>
    public ProjectService(HttpClient client)
        => this.client = client;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task AddProjectAsync(Project project)
        => throw new NotImplementedException();

    /// <summary>
    /// Gets all projects from the JSON file.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var projects = await this.client.GetFromJsonAsync<Project[]>(FILENAME);

        return projects;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public async Task<Project> GetProjectAsync(int projectId)
    {
        var projects = await this.client.GetFromJsonAsync<Project[]>(FILENAME);
        Project project = projects.First(p => p.Id == projectId);

        return project;
    }

    /// <summary>
    /// Gets all featured projects from the JSON file.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public async Task<IEnumerable<Project>> GetAllFeaturedProjectsAsync()
    {
        var projects = await this.client.GetFromJsonAsync<Project[]>(FILENAME);
        IEnumerable<Project> featuredProjects = projects.Where(p => p.IsFeatured);

        return featuredProjects;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task EditProjectAsync(Project project)
        => throw new NotImplementedException();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    public Task FeatureProjectAsync(int projectId)
        => throw new NotImplementedException();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task DeleteProjectAsync(Project project)
        => throw new NotImplementedException();
}
