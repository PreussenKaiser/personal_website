// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace PKaiser.Admin.Areas.Identity.Pages.Account;

/// <summary>
/// Handles validation for the Register form.
/// </summary>
public class RegisterModel : PageModel
{
    /// <summary>
    /// Signs the registering user in.
    /// </summary>
    private readonly SignInManager<IdentityUser> signInManager;

    /// <summary>
    /// Gets and processes data relating to the registering user.
    /// </summary>
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    /// The datastore to add the user to.
    /// </summary>
    private readonly IUserStore<IdentityUser> userStore;

    /// <summary>
    /// The datastore to add the user's email to.
    /// </summary>
    private readonly IUserEmailStore<IdentityUser> emailStore;

    /// <summary>
    /// Sends a confirmation email to the registering user.
    /// </summary>
    private readonly IEmailSender emailSender;

    /// <summary>
    /// Logs registration processes.
    /// </summary>
    private readonly ILogger<RegisterModel> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterModel"/> class.
    /// </summary>
    /// <param name="userManager">Signs the registering user in.</param>
    /// <param name="userStore">Gets and processes data relating to the registering user.</param>
    /// <param name="signInManager">The datastore to add the user to.</param>
    /// <param name="emailSender">Sends a confirmation email to the registering user.</param>
    /// <param name="logger">Logs registration processes.</param>
    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        IEmailSender emailSender,
        ILogger<RegisterModel> logger)
    {
        this.userManager = userManager;
        this.userStore = userStore;
        this.emailStore = this.GetEmailStore();
        this.signInManager = signInManager;
        this.emailSender = emailSender;
        this.logger = logger;
    }

    /// <summary>
    /// Gets or sets register form inputs.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    /// Gets or sets the url to return to when registration is completed.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets any external logins the user chooses.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    /// Represents validation data for <see cref="RegisterModel"/>.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the entered email.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the entered password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the re-entered password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Executes when the forms sends a GET request.
    /// </summary>
    /// <param name="returnUrl">The url to return to.</param>
    /// <returns>Whether the twask was completed or not.</returns>
    public async Task OnGetAsync(string returnUrl = null)
    {
        this.ReturnUrl = returnUrl;
        this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    /// <summary>
    /// Validates the Register form on a POST request.
    /// </summary>
    /// <param name="returnUrl">The url to return to if validation passes.</param>
    /// <returns>The previous page, or the Register form.</returns>
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= this.Url.Content("~/");
        this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync())
                                                       .ToList();

        if (this.ModelState.IsValid)
        {
            IdentityUser user = this.CreateUser();

            await this.userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
            await this.emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);

            IdentityResult result = await this.userManager.CreateAsync(user, this.Input.Password);

            if (result.Succeeded)
            {
                this.logger.LogInformation("User created a new account with password.");

                string userId = await this.userManager.GetUserIdAsync(user);
                string code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                string callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: this.Request.Scheme);

                await this.emailSender.SendEmailAsync(
                    Input.Email,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return this.RedirectToPage("RegisterConfirmation",
                        new
                        {
                            email = this.Input.Email,
                            returnUrl = returnUrl
                        });
                }
                else
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);

                    return this.LocalRedirect(returnUrl);
                }
            }

            foreach (var error in result.Errors)
                this.ModelState.AddModelError(string.Empty, error.Description);
        }

        // If we got this far, something failed, redisplay form
        return this.Page();
    }

    /// <summary>
    /// Creates a user in the data store.
    /// </summary>
    /// <returns>The created user.</returns>
    /// <exception cref="InvalidOperationException">When the user could not be created.</exception>
    private IdentityUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<IdentityUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    /// <summary>
    /// Gets the email store.
    /// </summary>
    /// <returns>The user email store.</returns>
    /// <exception cref="NotSupportedException">If the user store does not support emails.</exception>
    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!this.userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<IdentityUser>)this.userStore;
    }
}
