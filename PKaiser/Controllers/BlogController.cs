using Microsoft.AspNetCore.Mvc;

namespace PKaiser.Controllers;

/// <summary>
/// The controller that displays blog views.
/// </summary>
public class BlogController : Controller
{
    /// <summary>
    /// Displays the blogs index view.
    /// </summary>
    /// <returns>The blogs index view.</returns>
    public IActionResult Index()
        => this.View();
}
