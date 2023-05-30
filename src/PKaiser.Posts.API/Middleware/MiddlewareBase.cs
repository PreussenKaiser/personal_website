namespace PKaiser.Posts.API.Middleware;

/// <summary>
/// Base class for defining middleware.
/// </summary>
public abstract class MiddlewareBase
{
	/// <summary>
	/// The next action in the pipeline.
	/// </summary>
	protected readonly RequestDelegate next;

	/// <summary>
	/// Initializes <see cref="MiddlewareBase"/> with the next request.
	/// </summary>
	/// <param name="next">The next action in the pipeline.</param>
	public MiddlewareBase(RequestDelegate next)
	{
		this.next = next;
	}

	/// <summary>
	/// Executes the middleware.
	/// </summary>
	/// <param name="context">The current context.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public abstract Task InvokeAsync(HttpContext context);
}
