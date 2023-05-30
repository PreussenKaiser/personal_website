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
		string prefix)
	{
		builder.MapGroup(prefix);

		builder.MapPost(string.Empty, Post);
		builder.MapGet("{id:guid?}", Search);
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

		return Results.Created(string.Empty, project);
	}

	/// <summary>
	/// GET request to search for projects.
	/// </summary>
	/// <param name="projectRepository">The <see cref="IProjectRepository"/> to query projects from.</param>
	/// <param name="id"></param>
	/// <param name="page"></param>
	/// <param name="count"></param>
	/// <returns>The request's status.</returns>
	private static async Task<IResult> Search(
		[FromServices] IProjectRepository projectRepository,
		Guid? id = null,
		int page = 0,
		int count = 8)
	{
		IEnumerable<Project> projects = id is null
			? await projectRepository.SearchAsync(new AllSpecification<Project>(), page, count)
			: await projectRepository.SearchAsync(new IdSpecification<Project>((Guid)id), page, count);

		return Results.Ok(projects);
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
	/// 
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
