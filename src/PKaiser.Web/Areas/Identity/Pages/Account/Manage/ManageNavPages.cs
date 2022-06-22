// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace PKaiser.Web.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// Represents pages in application settings.
/// </summary>
public static class ManageNavPages
{
    /// <summary>
    /// Gets the url for the Index view.
    /// </summary>
    public static string Index
        => "Index";

    /// <summary>
    /// Gets the url for the RegisterAdmin view.
    /// </summary>
    public static string RegisterAdmin
        => "Register";

    /// <summary>
    /// Gets the url for the Email view.
    /// </summary>
    public static string Email
        => "Email";

    /// <summary>
    /// Gets the url for the ChangePassword view.
    /// </summary>
    public static string ChangePassword
        => "ChangePassword";

    /// <summary>
    /// Gets the url for the Entities view.
    /// </summary>
    public static string Entities
        => "Entities";

    /// <summary>
    /// Sets the Index view as the current navigated page.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns>A CSS selector indicating that the page is active or not.</returns>
    public static string IndexNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Index);

    /// <summary>
    /// Sets the RegisterAdmin view as the current navigated page.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns>A CSS selector indicating that the page is active or not.</returns>
    public static string RegisterAdminClass(ViewContext viewContext)
        => PageNavClass(viewContext, RegisterAdmin);

    /// <summary>
    /// Sets the Email view as the current navigated page.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns>A CSS selector indicating that the page is active or not.</returns>
    public static string EmailNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Email);

    /// <summary>
    /// Sets the ChangePassword view as the current navigated page.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns>A CSS selector indicating that the page is active or not.</returns>
    public static string ChangePasswordNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, ChangePassword);

    /// <summary>
    /// Sets the Entities view as the current navigated page.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns>A CSS selector indicating that the page is active or not.</returns>
    public static string EntitiesNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Entities);

    /// <summary>
    /// Sets the current navigated page to the supplied url.
    /// </summary>
    /// <param name="viewContext"></param>
    /// <param name="page">The page to set as active.</param>
    /// <returns>
    /// A CSS selector indicating that the page is active.
    /// Otherwise, no selector is returned.
    /// </returns>
    public static string PageNavClass(ViewContext viewContext, string page)
    {
        string activePage = viewContext.ViewData["ActivePage"] as string
            ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase)
            ? "active"
            : null;
    }
}
