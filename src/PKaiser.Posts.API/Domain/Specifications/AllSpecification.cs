using PKaiser.Posts.API.Domain.Entities;
using System.Linq.Expressions;

namespace PKaiser.Posts.API.Domain.Specifications;

/// <summary>
/// Specification which always passes.
/// </summary>
/// <typeparam name="TEntity">The entity to match the specification against.</typeparam>
public sealed class AllSpecification<TEntity> : Specification<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc/>
	public override Expression<Func<TEntity, bool>> ToExpression()
		=> entity => true;
}
