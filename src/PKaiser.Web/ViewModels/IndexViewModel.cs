using PKaiser.Core.Models;
using PKaiser.Core.Services;

namespace PKaiser.Web.ViewModels;

/// <summary>
/// The view model for the '/' page.
/// </summary>
public sealed class IndexViewModel : ViewModelBase
{
	/// <summary>
	/// The service to get featured projects with.
	/// </summary>
	private readonly IProjectService projectService;

	/// <summary>
	/// Backing field for featured projects.
	/// </summary>
	private IEnumerable<Project>? featuredProjects;

	/// <summary>
	/// Initializes the <see cref="IndexViewModel"/> class.
	/// </summary>
	/// <param name="projectService">The service to get featured projects with.</param>
	public IndexViewModel(IProjectService projectService)
	{
		this.projectService = projectService;

		Task.Run(async ()
			=> this.FeaturedProjects = await projectService.GetFeaturedAsync());
	}

	/// <summary>
	/// Featured projects.
	/// </summary>
	public IEnumerable<Project>? FeaturedProjects
	{
		get => this.featuredProjects;
		private set => this.SetProperty(ref this.featuredProjects, value);
	}
}
