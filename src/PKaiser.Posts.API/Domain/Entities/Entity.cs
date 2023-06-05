namespace PKaiser.Posts.API.Domain.Entities;

/// <summary>
/// Represents en entity.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Initializes the <see cref="Entity"/> class.
	/// </summary>
	public Entity()
	{
		this.Id = Guid.NewGuid();
	}

	/// <summary>
	/// Gets the entity's unique identifier.
	/// </summary>
	public Guid Id { get; init; }
}
