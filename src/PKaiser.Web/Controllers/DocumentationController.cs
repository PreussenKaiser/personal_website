using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PKaiser.Web.Controllers;

/// <summary>
/// The controller that renders documentation views.
/// </summary>
[Authorize]
public class DocumentationController : Controller
{
    /// <summary>
    /// Displays the documentation index view.
    /// </summary>
    /// <returns>The documentation index view.</returns>
    public IActionResult Index()
        => this.View();
}
