using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// Implements project query methods.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Creates a project in the service asynchronously.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task AddProjectAsync(Project project);

    /// <summary>
    /// Gets all project from the service asynchronously.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public Task<IEnumerable<Project>> GetAllProjectsAsync();

    /// <summary>
    /// Gets all featured projects from the service asynchronously.
    /// </summary>
    /// <returns>An enumerable of featured projects.</returns>
    public Task<IEnumerable<Project>> GetAllFeaturedProjectsAsync();

    /// <summary>
    /// Gets a project from the service asynchronously.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to get.</param>
    /// <returns>The found project.</returns>
    public Task<Project> GetProjectAsync(int projectId);

    /// <summary>
    /// Updates a project in the service asynchronously.
    /// </summary>
    /// <param name="project">The model containing which project to update (using the identifier) as well as it's values.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task EditProjectAsync(Project project);

    /// <summary>
    /// Deletes a project from the service asynchronously.
    /// </summary>
    /// <param name="project">The project to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task DeleteProjectAsync(Project project);
}
