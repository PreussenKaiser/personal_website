using PKaiser.Core.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PKaiser.Infrastructure.Data;

/// <summary>
/// The class that represents the context for the local MSSQL database.
/// </summary>
public class WebsiteContext : IdentityDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebsiteContext"/> class.
    /// </summary>
    /// <param name="options"></param>
    public WebsiteContext(DbContextOptions<WebsiteContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets projects from the context.
    /// </summary>
    public DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets blogs from the context.
    /// </summary>
    public DbSet<Blog> Blogs { get; set; }
}