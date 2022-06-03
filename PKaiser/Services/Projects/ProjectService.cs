using PKaiser.Data;
using PKaiser.Models;

namespace PKaiser.Services.Projects;

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

    public Task AddProjectAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProjectAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetAllProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProjectAsync(int projectId)
    {
        throw new NotImplementedException();
    }

    public Task HighlightProjectAsync(int projectId)
    {
        throw new NotImplementedException();
    }
}
