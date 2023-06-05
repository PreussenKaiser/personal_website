using PKaiser.Web.Models;
using PKaiser.Web.Services;

namespace PKaiser.Web.ViewModels;

/// <summary>
/// The view model for the 'projects/{id:int}' page.
/// </summary>
public sealed class ProjectDetailViewModel : ViewModelBase
{
	/// <summary>
	/// The service to get details for <see cref="Project"/> with.
	/// </summary>
	private readonly IProjectService projectService;

	/// <summary>
	/// Backing field for the project to detail.
	/// </summary>
	private Project? project;

	/// <summary>
	/// Initializes the <see cref="ProjectDetailViewModel"/> class.
	/// </summary>
	/// <param name="projectService">The service to get details for <see cref="Project"/> with.</param>
	public ProjectDetailViewModel(IProjectService projectService)
	{
		this.projectService = projectService;
	}

	/// <summary>
	/// Gets the project to detail.
	/// </summary>
	public Project? Project
	{
		get => this.project;
		private set => this.SetProperty(ref this.project, value);
	}

	/// <summary>
	/// Instantiates <see cref="Project"/> with an identifier..
	/// </summary>
	/// <param name="id"> value referencing <see cref="Project.Id"/>.</param>
	/// <returns>Whether the task was completed or not.</returns>
	public async Task InitializeProject(Guid id)
		=> this.Project = await this.projectService.GetAsync(id);
}
