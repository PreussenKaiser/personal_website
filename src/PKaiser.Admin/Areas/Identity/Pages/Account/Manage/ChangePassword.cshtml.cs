// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PKaiser.Admin.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// Represents the model for changing a password.
/// </summary>
public class ChangePasswordModel : PageModel
{
    /// <summary>
    /// For editing a user's password.
    /// </summary>
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    /// Signs the user back in after a successful password change.
    /// </summary>
    private readonly SignInManager<IdentityUser> signInManager;

    /// <summary>
    /// Logs form processes.
    /// </summary>
    private readonly ILogger<ChangePasswordModel> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
    /// </summary>
    /// <param name="userManager">For editing a user's password</param>
    /// <param name="signInManager"></param>
    /// <param name="logger"></param>
    public ChangePasswordModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<ChangePasswordModel> logger)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.logger = logger;
    }


    /// <summary>
    /// The input to validate.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    /// The status of the form.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Represents form values.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the password to modify.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets a repeat of the new password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Executed when the form makes a GET request.
    /// </summary>
    /// <returns>The view for changing a user's password.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");

        bool hasPassword = await this.userManager.HasPasswordAsync(user);

        if (!hasPassword)
            return this.RedirectToPage("./SetPassword");

        return this.Page();
    }

    /// <summary>
    /// Executed when the form makes a POST request.
    /// </summary>
    /// <returns>The view for changing a user's password.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!this.ModelState.IsValid)
            return this.Page();

        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");

        IdentityResult changePasswordResult = await this.userManager.ChangePasswordAsync(user, this.Input.OldPassword, this.Input.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            foreach (IdentityError error in changePasswordResult.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }

        await this.signInManager.RefreshSignInAsync(user);
        this.logger.LogInformation("User changed their password successfully.");
        this.StatusMessage = "Your password has been changed.";

        return this.RedirectToPage();
    }
}
