using PKaiser.Core.Models;

namespace PKaiser.Core.Services;

/// <summary>
/// Defines read and write operations against a data source for <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of entity to query.</typeparam>
public interface IRepository<TEntity>
	where TEntity : IModel<int>
{
	/// <summary>
	/// Creates a <typeparamref name="TEntity"/> in the repository.
	/// </summary>
	/// <param name="entity">The <typeparamref name="TEntity"/> to create.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task CreateAsync(TEntity entity);

	/// <summary>
	/// Gets all instances of <typeparamref name="TEntity"/> in the repository.
	/// </summary>
	/// <returns>An enumerable, null if none were found.</returns>
	Task<IEnumerable<TEntity>?> GetAllAsync();

	/// <summary>
	/// Gets a <typeparamref name="TEntity"/> by it's unique identifier.
	/// </summary>
	/// <param name="id">A value referencing <see cref="TEntity.Id"/>.</param>
	/// <returns>The found <typeparamref name="TEntity"/>, null if none were found.</returns>
	Task<TEntity?> GetAsync(int id);

	/// <summary>
	/// Updates a <typeparamref name="TEntity"/> in the repository.
	/// </summary>
	/// <param name="entity">The <typeparamref name="TEntity"/> to update.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task UpdateAsync(TEntity entity);

	/// <summary>
	/// Deletes a <typeparamref name="TEntity"/> frm the repository.
	/// </summary>
	/// <param name="id">A value referencing <see cref="TEntity.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task DeleteAsync(int id);
}
