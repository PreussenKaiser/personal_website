using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using AuthenticationOptions = PKaiser.Posts.API.Options.AuthenticationOptions;

namespace PKaiser.Posts.API.Middleware;

/// <summary>
/// Middleware for authenticating with an API key.
/// </summary>
public sealed class KeyMiddleware : MiddlewareBase
{
	/// <summary>
	/// Configuration options for authentication.
	/// </summary>
	private readonly AuthenticationOptions options;

	/// <summary>
	/// Initializes <see cref="KeyMiddleware"/> with the next request.
	/// </summary>
	/// <param name="next"></param>
	public KeyMiddleware(RequestDelegate next, IOptions<AuthenticationOptions> options)
		: base(next)
	{
		this.options = options.Value;
	}

	/// <inheritdoc/>
	public override async Task InvokeAsync(HttpContext context)
	{
		bool apiKeyProvided = context.Request.Headers.TryGetValue("Key", out StringValues requestApiKey);
		if (!apiKeyProvided)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("An API key is required.");

			return;
		}

		bool correctKey = this.options.Key.Equals(apiKeyProvided);
		if (!correctKey)
		{
			context.Response.StatusCode= StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Unauthorized.");

			return;
		}

		await this.next(context);
	}
}
