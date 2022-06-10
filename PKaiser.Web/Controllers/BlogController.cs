using PKaiser.Core.Services;

using Microsoft.AspNetCore.Mvc;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller that displays blog views.
/// </summary>
public class BlogController : Controller
{
    /// <summary>
    /// The service to get blogs with.
    /// </summary>
    private readonly IBlogService blogService;

    /// <summary>
    /// Logs blog view processes.
    /// </summary>
    private readonly ILogger<BlogController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlogController"/> class.
    /// </summary>
    /// <param name="blogService">The service to get blogs with.</param>
    /// <param name="logger">Logs blog view processes.</param>
    public BlogController(IBlogService blogService,
                          ILogger<BlogController> logger)
    {
        this.blogService = blogService;
        this.logger = logger;
    }

    /// <summary>
    /// Displays the blogs index view.
    /// </summary>
    /// <returns>The blogs index view.</returns>
    public IActionResult Index()
        => this.View();
}
