namespace PKaiser.Posts.API.Options;

/// <summary>
/// Configuration for API authentication.
/// </summary>
public sealed class AuthenticationOptions
{
	/// <summary>
	/// The name of the hierarchy.
	/// </summary>
	public const string AUTHENTICATION = "Authentication";

	/// <summary>
	/// Gets the API key.
	/// </summary>
	public required string Key { get; init; }
}
