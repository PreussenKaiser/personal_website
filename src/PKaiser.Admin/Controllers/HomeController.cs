using PKaiser.Admin.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PKaiser.Admin.Controllers;

/// <summary>
/// The controller which displays home views.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    public HomeController()
    {
    }

    /// <summary>
    /// Renders the index view.
    /// </summary>
    /// <returns>The index view.</returns>
    public IActionResult Index()
        => this.View();

    /// <summary>
    /// Renders the error view.
    /// </summary>
    /// <returns>The error view.</returns>
    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
        => this.View(new ErrorViewModel 
                    {
                        RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
                    });
}