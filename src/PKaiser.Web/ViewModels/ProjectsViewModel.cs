using PKaiser.Web.Models;
using PKaiser.Web.Services;

namespace PKaiser.Web.ViewModels;

/// <summary>
/// The view moel for the '/projects' page.
/// </summary>
public sealed class ProjectsViewModel : ViewModelBase
{
	/// <summary>
	/// The service to get all projects with.
	/// </summary>
	private readonly IProjectService projectService;

	/// <summary>
	/// Backing field for projects to display.
	/// </summary>
	private IEnumerable<Project>? projects;

	/// <summary>
	/// Initializes the <see cref="ProjectsViewModel"/> class.
	/// </summary>
	/// <param name="projectService">The service to query projects with.</param>
	public ProjectsViewModel(IProjectService projectService)
	{
		this.projectService = projectService;

		Task.Run(async () => this.Projects = await this.projectService.GetPaginatedAsync());
	}

	/// <summary>
	/// Gets projects to display.
	/// </summary>
	public IEnumerable<Project>? Projects
	{
		get => this.projects;
		private set => this.SetProperty(ref this.projects, value);
	}
}
