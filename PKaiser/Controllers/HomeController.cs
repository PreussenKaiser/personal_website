using Microsoft.AspNetCore.Mvc;
using PKaiser.Models;
using System.Diagnostics;

namespace PKaiser.Controllers;

/// <summary>
/// The controller which displays home views.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// The logger for home views.
    /// </summary>
    private readonly ILogger<HomeController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">The logger for home views.</param>
    public HomeController(ILogger<HomeController> logger)
        => this.logger = logger;

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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => this.View(new ErrorViewModel 
                    {
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    });
}