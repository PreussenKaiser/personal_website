using PKaiser.Posts.API.Domain.Entities;
using PKaiser.Posts.API.Domain.Specifications;

namespace PKaiser.Posts.API.Domain.Repositories;

/// <summary>
/// Implements project query methods.
/// </summary>
public interface IProjectRepository
{
	/// <summary>
	/// Adds a project to the repository.
	/// </summary>
	/// <param name="project">The project to add.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task PostAsync(Project project);

	/// <summary>
	/// Searches for the project(s) by a specification.
	/// </summary>
	/// <param name="specification">Search criteria.</param>
	/// <param name="skip">The current page.</param>
	/// <param name="take">The amount of projects to return.</param>
	/// <returns>Projects which meet the specification.</returns>
	Task<IEnumerable<Project>> SearchAsync(Specification<Project> specification, int skip, int take);

	/// <summary>
	/// Edits an existing project.
	/// </summary>
	/// <param name="project">The project's identifier as well as it's new values.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task EditAsync(Project project);

	/// <summary>
	/// Deletes an existing project.
	/// </summary>
	/// <param name="specification">The specification to delete the project by.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task RemoveAsync(Specification<Project> specification);
}
