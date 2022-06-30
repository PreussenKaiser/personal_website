// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PKaiser.Admin.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// Represents the model for validating the Index view.
/// </summary>
public class IndexModel : PageModel
{
    /// <summary>
    /// The user manager to edit user data with.
    /// </summary>
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    /// For signing the user back in when they edit their information.
    /// </summary>
    private readonly SignInManager<IdentityUser> signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="userManager">The user manager to edit user data with.</param>
    /// <param name="signInManager">For signing the user back in when they edit their information.</param>
    public IndexModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    /// <summary>
    /// Gets or sets the user's current user name.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the form's current status.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Gets or sets the inputs to validate.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    /// Represents validation data for the Index form.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Sets inputs when the page is loaded.
    /// </summary>
    /// <param name="user">The user to get input values from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task LoadAsync(IdentityUser user)
    {
        string userName = await this.userManager.GetUserNameAsync(user);
        string phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

        this.Username = userName;

        this.Input = new InputModel
        {
            PhoneNumber = phoneNumber
        };
    }

    /// <summary>
    /// Executes when the form sends a GET request.
    /// </summary>
    /// <returns>The page for editing user information.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");

        await this.LoadAsync(user);

        return this.Page();
    }

    /// <summary>
    /// Validates the form and sets user data.
    /// </summary>
    /// <returns>The page for editing user information.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");

        if (!this.ModelState.IsValid)
        {
            await this.LoadAsync(user);

            return this.Page();
        }

        string phoneNumber = await userManager.GetPhoneNumberAsync(user);

        if (this.Input.PhoneNumber != phoneNumber)
        {
            IdentityResult setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);

            if (!setPhoneResult.Succeeded)
            {
                this.StatusMessage = "Unexpected error when trying to set phone number.";

                return this.RedirectToPage();
            }
        }

        await this.signInManager.RefreshSignInAsync(user);
        this.StatusMessage = "Your profile has been updated";

        return this.RedirectToPage();
    }
}
