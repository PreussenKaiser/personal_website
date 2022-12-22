using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// Implements project query methods.
/// </summary>
public interface IProjectService : IRepository<Project>
{
    /// <summary>
    /// Gets all featured projects from the service.
    /// </summary>
    /// <returns>All projects with <see cref="Project.IsFeatured"/> set to true.</returns>
    Task<IEnumerable<Project>?> GetFeaturedAsync();
}
