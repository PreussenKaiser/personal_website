// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace PKaiser.Areas.Identity.Pages.Account;

/// <summary>
/// The class that handles business login for the register form.
/// </summary>
public class RegisterModel : PageModel
{
    /// <summary>
    /// 
    /// </summary>
    private readonly SignInManager<IdentityUser> signInManager;

    /// <summary>
    /// 
    /// </summary>
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    /// 
    /// </summary>
    private readonly IUserStore<IdentityUser> userStore;

    /// <summary>
    /// 
    /// </summary>
    private readonly IUserEmailStore<IdentityUser> emailStore;

    /// <summary>
    /// 
    /// </summary>
    private readonly IEmailSender emailSender;

    /// <summary>
    /// Logs registration processes.
    /// </summary>
    private readonly ILogger<RegisterModel> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterModel"/> class.
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="userStore"></param>
    /// <param name="signInManager"></param>
    /// <param name="logger">Logs registration processes.</param>
    /// <param name="emailSender"></param>
    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender)
    {
        this.userManager = userManager;
        this.userStore = userStore;
        this.emailStore = this.GetEmailStore();
        this.signInManager = signInManager;
        this.logger = logger;
        this.emailSender = emailSender;
    }

    /// <summary>
    /// Gets or sets register form inputs.
    /// 
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    /// The class that represents inputs for the register form.
    /// 
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the entered email.
        /// 
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the entered password.
        /// 
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the re-entered password.
        /// 
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    public async Task OnGetAsync(string returnUrl = null)
    {
        this.ReturnUrl = returnUrl;
        this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= this.Url.Content("~/");
        this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (this.ModelState.IsValid)
        {
            var user = this.CreateUser();

            await this.userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
            await this.emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);

            var result = await this.userManager.CreateAsync(user, this.Input.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("User created a new account with password.");

                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: this.Request.Scheme);

                await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
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
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
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
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
        if (!this.userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<IdentityUser>)this.userStore;
    }
}
