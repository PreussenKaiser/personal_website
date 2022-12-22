using PKaiser.Infrastructure.Extensions;
using PKaiser.Core.Models;
using PKaiser.Core.Services;
using System.Net.Http.Json;

namespace PKaiser.Infrastructure.Services;

/// <summary>
/// Queries projects using a local JSON file.
/// </summary>
public sealed class ProjectService : IProjectService
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
    /// Initializes a new instance of the <see cref="ProjectService"/> class.
    /// </summary>
    /// <param name="client">The http client to read and write JSON with.</param>
    public ProjectService(HttpClient client)
        => this.client = client;

    /// <summary>
    /// Adds a <see cref="Project"/> to the JSON document.
    /// </summary>
    /// <param name="entity">The <see cref="Project"/> to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task CreateAsync(Project entity)
        => await this.client.PostAsJsonAsync(FILENAME, entity);

    /// <summary>
    /// Gets all <see cref="Project"/> from the JSON document.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public async Task<IEnumerable<Project>?> GetAllAsync()
    {
        var projects = await this.client.GetFromJsonAsync<Project[]>(FILENAME);

        return projects;
    }

    /// <summary>
    /// Gets all featured projects from the JSON document.
    /// </summary>
    /// <returns>Featured projects from the document.</returns>
    public async Task<IEnumerable<Project>?> GetFeaturedAsync()
    {
        IEnumerable<Project>? projects = await this.GetAllAsync();

        projects = projects?.Where(p => p.IsFeatured);

        return projects;
    }

    /// <summary>
    /// Gets a <see cref="Project"/> from the JSON document.
    /// </summary>
    /// <param name="id">A value referencing <see cref="Project.Id"/>.</param>
    /// <returns>The found project.</returns>
    public async Task<Project?> GetAsync(int id)
    {
        IEnumerable<Project>? projects = await this.GetAllAsync();
        Project? foundProject = projects?.FirstOrDefault(p => p.Id == id);

        return foundProject;
    }

    /// <summary>
    /// Updates a <see cref="Project"/> in the JSON document.
    /// </summary>
    /// <param name="entity">The <see cref="Project"/> to update.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task UpdateAsync(Project entity)
        => await this.client.PutAsJsonAsync(FILENAME, entity);

    /// <summary>
    /// Deletes a <see cref="Project"/> from the JSON document.
    /// </summary>
    /// <param name="id">A value referencing <see cref="Project.Id"/>.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task DeleteAsync(int id)
	{
		Project? project = await this.GetAsync(id);

		if (project is null)
			return;

        await this.client.DeleteAsJsonAsync(FILENAME, project);
	}
}
