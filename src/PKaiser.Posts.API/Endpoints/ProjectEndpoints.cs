using Microsoft.AspNetCore.Mvc;
using PKaiser.Posts.API.Domain.Entities;
using PKaiser.Posts.API.Domain.Repositories;
using PKaiser.Posts.API.Domain.Specifications;

namespace PKaiser.Posts.API.Endpoints;

/// <summary>
/// Endpoints for projects.
/// </summary>
public static class ProjectEndpoints
{
	/// <summary>
	/// Adds project endpoints to a route.
	/// </summary>
	/// <param name="builder">The route to add endpoints to.</param>
	/// <param name="prefix">Endpoint group name,</param>
	/// <returns>Configured endpoints.</returns>
	public static IEndpointRouteBuilder MapProjects(
		this IEndpointRouteBuilder builder,
		in string prefix)
	{
		builder.MapGroup(prefix);

		builder.MapPost(string.Empty, Post);
		builder.MapGet("{id:guid}", Get);
		builder.MapGet("page/{page:int}/count/{count:int}", GetPaginated);
		builder.MapPut(string.Empty, Edit);
		builder.MapDelete("{id:guid}", Remove);

		return builder;
	}

	/// <summary>
	/// POST request for adding a <see cref="Project"/>.
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to add the <see cref="Project"/> to.</param>
	/// <param name="project">The <see cref="Project"/> to add.</param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> Post(
		[FromServices] IProjectRepository projectRepository,
		Project project)
	{
		await projectRepository.PostAsync(project);

		return Results.Created(project.Id.ToString(), project);
	}

	/// <summary>
	/// GET request to retrieve a paginated list of projects.
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to query projects from.</param>
	/// <param name="page">The page to get projects from.</param>
	/// <param name="count">The amount of projects to get.</param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> GetPaginated(
		[FromServices] IProjectRepository projectRepository,
		int page = 0,
		int count = 8)
	{
		IEnumerable<Project> projects = await projectRepository.SearchAsync(new AllSpecification<Project>(), page, count);

		return Results.Ok(projects);
	}

	/// <summary>
	/// GET request to retrieve a single project by it's identifier.
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to query the project from.</param>
	/// <param name="id">The project's unique identifier.</param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> Get(
		[FromServices] IProjectRepository projectRepository,
		Guid id)
	{
		Project? project = (await projectRepository
			.SearchAsync(new IdSpecification<Project>(id), 0, 1))
			.FirstOrDefault();

		return project is null
			? Results.BadRequest($"Could not find the project.")
			: Results.Ok(project);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to update the <see cref="Project"/> with.</param>
	/// <param name="project"></param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> Edit(
		[FromServices] IProjectRepository projectRepository,
		Project project)
	{
		await projectRepository.EditAsync(project);

		return Results.Ok();
	}

	/// <summary>
	/// DELETE request to remove a project.
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to delete the <see cref="Project"/> from.</param>
	/// <param name="id"></param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> Remove(
		[FromServices] IProjectRepository projectRepository,
		Guid id)
	{
		await projectRepository.RemoveAsync(new IdSpecification<Project>(id));

		return Results.Ok();
	}
}
