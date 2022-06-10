using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// The interface which implements project query methods.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Adds a project to the service.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task AddProjectAsync(Project project);

    /// <summary>
    /// Gets all project from the service.
    /// </summary>
    /// <returns>A list of projects.</returns>
    public Task<List<Project>> GetAllProjectsAsync();

    /// <summary>
    /// Gets a project from the service.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to get.</param>
    /// <returns>The found project.</returns>
    public Task<Project> GetProjectAsync(int projectId);

    /// <summary>
    /// Highlights a given podcast in the service.
    /// </summary>
    /// <param name="projectId">The unique identifier of the podcast to highlight.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task HighlightProjectAsync(int projectId);

    /// <summary>
    /// Deletes a project from the service.
    /// </summary>
    /// <param name="project">The project to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task DeleteProjectAsync(Project project);
}
