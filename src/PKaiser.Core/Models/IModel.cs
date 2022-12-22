namespace PKaiser.Core.Models;

/// <summary>
/// Defines data with a unique identifier.
/// </summary>
/// <typeparam name="TId">The type of identifier.</typeparam>
public interface IModel<TId>
	where TId : IEquatable<TId>
{
	/// <summary>
	/// Gets the model's unique identifier.
	/// </summary>
	TId Id { get; }
}
