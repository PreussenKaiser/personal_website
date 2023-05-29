using PKaiser.Posts.API.Domain.Entities;
using System.Linq.Expressions;

namespace PKaiser.Posts.API.Domain.Specifications;

/// <summary>
/// Matches <typeparamref name="TEntity"/> against an identifier.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public sealed class IdSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// The identifier to search for.
	/// </summary>
	private readonly Guid id;

	/// <summary>
	/// Initializes with identifier.
	/// </summary>
	/// <param name="id">The identifier to search for.</param>
	public IdSpecification(Guid id)
	{
		ArgumentNullException.ThrowIfNull(id);

		this.id = id;
	}

	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
		=> entity => entity.Id == this.id;
}
