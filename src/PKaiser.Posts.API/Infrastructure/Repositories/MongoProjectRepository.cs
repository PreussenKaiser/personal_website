using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PKaiser.Posts.API.Domain.Entities;
using PKaiser.Posts.API.Domain.Repositories;
using PKaiser.Posts.API.Domain.Specifications;
using PKaiser.Posts.API.Infrastructure.Persistence;
using PKaiser.Posts.API.Options;

namespace PKaiser.Posts.API.Infrastructure.Repositories;

/// <summary>
/// Execute commands and queries against a MongoDB database.
/// </summary>
public sealed class MongoProjectRepository : IProjectRepository
{
	/// <summary>
	/// The MongoDB database.
	/// </summary>
	private readonly PostsContext context;

	/// <summary>
	/// Initializes <see cref="MongoProjectRepository"/> with <see cref="PostsContext"/>.
	/// </summary>
	/// <param name="context">The MongoDB database.</param>
	public MongoProjectRepository(PostsContext context)
	{
		this.context = context;
	}

	/// <inheritdoc/>
	public async Task PostAsync(Project project)
	{
		await this.context.Projects.InsertOneAsync(project);
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Project>> SearchAsync(
		Specification<Project> specification,
		int skip = 0,
		int take = 8)
	{
		IEnumerable<Project> projects = await this.context.Projects
			.Find(specification.ToExpression())
			.ToListAsync();

		return projects;
	}

	/// <inheritdoc/>
	public async Task EditAsync(Project project)
	{
		await this.context.Projects.ReplaceOneAsync(p => p.Id == project.Id, project);
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Specification<Project> specification)
	{
		await this.context.Projects.DeleteOneAsync(specification.ToExpression());
	}
}
