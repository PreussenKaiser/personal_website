using PKaiser.Infrastructure.Data;

using PKaiser.Core.Models;
using PKaiser.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace PKaiser.Infrastructure.Services;

/// <summary>
/// The service that gets projects from the local MSSQL database.
/// </summary>
public class ProjectService : IProjectService
{
    /// <summary>
    /// The database context to get projects from.
    /// </summary>
    private readonly WebsiteContext database;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectService"/> class.
    /// </summary>
    /// <param name="database">The database context to get projects from.</param>
    public ProjectService(WebsiteContext database)
        => this.database = database;

    /// <summary>
    /// Creates a project in the local database asynchronusly.
    /// </summary>
    /// <param name="project">The project to create.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task AddProjectAsync(Project project)
    {
        await this.database.Projects.AddAsync(project);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Gets all projects rom the local database asynchronously.
    /// </summary>
    /// <returns>An enumerable of projects.</returns>
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        IEnumerable<Project> projects = await this.database.Projects.ToListAsync();

        return projects;
    }

    /// <summary>
    /// Gets all featured projects from the database asynchronously.
    /// </summary>
    /// <returns>An enumerable of featured projects.</returns>
    public async Task<IEnumerable<Project>> GetAllFeaturedProjectsAsync()
    {
        List<Project> projects = await this.database.Projects.ToListAsync();
        IEnumerable<Project> featuredProjects = this.database.Projects.Where(p => p.IsFeatured);

        return featuredProjects;
    }

    /// <summary>
    /// Gets a project from te local database.
    /// </summary>
    /// <param name="projectId">The identifier of the project o get.</param>
    /// <returns>The first project in the database that matches the identifier.</returns>
    public async Task<Project> GetProjectAsync(int projectId)
    {
        Project project = await this.database.Projects.FindAsync(projectId);

        return project;
    }

    /// <summary>
    /// Updates a project in the database asynchronously.
    /// </summary>
    /// <param name="project">The model containing which project to update (using the identifier) as well as it's values.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task EditProjectAsync(Project project)
    {
        this.database.Projects.Update(project);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a project from the local database.
    /// </summary>
    /// <param name="project">The project to delete.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public async Task DeleteProjectAsync(Project project)
    {
        if (project is null)
            return;

        this.database.Projects.Remove(project);
        await this.database.SaveChangesAsync();
    }

    /// <summary>
    /// Sets a project as highlighted in the local database.
    /// </summary>
    /// <param name="projectId">The identifier of the project to hightlight.</param>
    /// <returns>Whether the task was completed or not.</returns>
    public Task FeatureProjectAsync(int projectId)
    {
        throw new NotImplementedException();
    }
}
