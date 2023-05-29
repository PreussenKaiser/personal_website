namespace PKaiser.Posts.API.Domain.Entities;

/// <summary>
/// Represents en entity.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Gets the entity's unique identifier.
	/// </summary>
	public required Guid Id { get; init; }
}
