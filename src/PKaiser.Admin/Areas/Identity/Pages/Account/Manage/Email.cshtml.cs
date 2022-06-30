// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace PKaiser.Admin.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// Represents validation for the Email view.
/// </summary>
public class EmailModel : PageModel
{
    /// <summary>
    /// The user manager to modify a user's email with.
    /// </summary>
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    /// The API to send a confirmation email with.
    /// </summary>
    private readonly IEmailSender emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <param name="userManager">The user manager to modify a user's email with.</param>
    /// <param name="emailSender">The API to send a confimation email with.</param>
    public EmailModel(
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender)
    {
        this.userManager = userManager;
        this.emailSender = emailSender;
    }

    /// <summary>
    /// Gets or sets the email to edit.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets whether the email has been confirmed or not.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the current status of the form.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Gets or sets the model containing inputs.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    /// Represents the model which holds Email inputs and their validation.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the new email to use.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }
    }

    /// <summary>
    /// Fills out inputs when the page loads.
    /// </summary>
    /// <param name="user">The user to get input values from.</param>
    /// <returns>Whether the task was completed or not.</returns>
    private async Task LoadAsync(IdentityUser user)
    {
        string email = await this.userManager.GetEmailAsync(user);

        this.Email = email;

        this.Input = new InputModel
        {
            NewEmail = email,
        };

        this.IsEmailConfirmed = await this.userManager.IsEmailConfirmedAsync(user);
    }

    /// <summary>
    /// Executes when the form sends a GET request.
    /// </summary>
    /// <returns>The view for changing your email.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");

        await this.LoadAsync(user);

        return this.Page();
    }

    /// <summary>
    /// Changes the user's email address when the form sends a POST request.
    /// </summary>
    /// <returns>The view to change a user's email with.</returns>
    public async Task<IActionResult> OnPostChangeEmailAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");

        if (!this.ModelState.IsValid)
        {
            await this.LoadAsync(user);

            return this.Page();
        }

        string email = await this.userManager.GetEmailAsync(user);

        if (this.Input.NewEmail != email)
        {
            string userId = await this.userManager.GetUserIdAsync(user);
            string code = await this.userManager.GenerateChangeEmailTokenAsync(user, this.Input.NewEmail);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            string callbackUrl = this.Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                protocol: this.Request.Scheme);

            await this.emailSender.SendEmailAsync(
                this.Input.NewEmail,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.StatusMessage = "Confirmation link to change email sent. Please check your email.";
            return this.RedirectToPage();
        }

        this.StatusMessage = "Your email is unchanged.";
        return this.RedirectToPage();
    }

    /// <summary>
    /// Sends a verification email when the form sends a POST request.
    /// </summary>
    /// <returns>The view for changing a user's email.</returns>
    public async Task<IActionResult> OnPostSendVerificationEmailAsync()
    {
        IdentityUser user = await this.userManager.GetUserAsync(this.User);

        if (user is null)
            return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");

        if (!this.ModelState.IsValid)
        {
            await this.LoadAsync(user);

            return this.Page();
        }

        string userId = await this.userManager.GetUserIdAsync(user);
        string email = await this.userManager.GetEmailAsync(user);
        string code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        string callbackUrl = this.Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = code },
            protocol: this.Request.Scheme);

        await this.emailSender.SendEmailAsync(
            email,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        this.StatusMessage = "Verification email sent. Please check your email.";

        return this.RedirectToPage();
    }
}
