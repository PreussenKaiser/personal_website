using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PKaiser.Posts.API.Domain.Entities;
using PKaiser.Posts.API.Domain.Repositories;
using PKaiser.Posts.API.Domain.Specifications;
using PKaiser.Posts.API.Options;

namespace PKaiser.Posts.API.Infrastructure.Repositories;

/// <summary>
/// Execute commands and queries against a MongoDB database.
/// </summary>
public sealed class MongoProjectRepository : IProjectRepository
{
	/// <summary>
	/// The collection of projects in the repository.
	/// </summary>
	private readonly IMongoCollection<Project> projects;

	/// <summary>
	/// Initializes <see cref="MongoProjectRepository"/> with configuration.
	/// </summary>
	/// <param name="options">Configuration options.</param>
	public MongoProjectRepository(IOptions<ProjectDatabaseOptions> options)
	{
		ProjectDatabaseOptions optionsParsed = options.Value;

		MongoClient client = new(optionsParsed.ConnectionString);
		IMongoDatabase database = client.GetDatabase(optionsParsed.DatabaseName);

		this.projects = database.GetCollection<Project>(optionsParsed.CollectionName);
	}

	/// <inheritdoc/>
	public async Task PostAsync(Project project)
	{
		await this.projects.InsertOneAsync(project);
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Project>> SearchAsync(
		Specification<Project> specification, int skip, int take)
	{
		IEnumerable<Project> projects = await this.projects
			.Find(specification.ToExpression())
			.ToListAsync();

		return projects;
	}

	/// <inheritdoc/>
	public async Task EditAsync(Project project)
	{
		await this.projects.ReplaceOneAsync(p => p.Id == project.Id, project);
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Specification<Project> specification)
	{
		await this.projects.DeleteOneAsync(specification.ToExpression());
	}
}
