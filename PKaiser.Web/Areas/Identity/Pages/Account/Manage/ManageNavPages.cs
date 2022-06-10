// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PKaiser.Web.Controllers;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace PKaiser.Web.Areas.Identity.Pages.Account.Manage;

/// <summary>
/// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
/// directly from your code. This API may change or be removed in future releases.
/// </summary>
public static class ManageNavPages
{
    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string Index
        => "Index";

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string Email
        => "Email";

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string ChangePassword
        => "ChangePassword";

    /// <summary>
    /// Gets the url for the entities view.
    /// </summary>
    public static string Entities
        => "Entities";

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string IndexNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Index);

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string EmailNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Email);

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string ChangePasswordNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, ChangePassword);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewContext"></param>
    /// <returns></returns>
    public static string EntitiesNavClass(ViewContext viewContext)
        => PageNavClass(viewContext, Entities);

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static string PageNavClass(ViewContext viewContext, string page)
    {
        string activePage = viewContext.ViewData["ActivePage"] as string
            ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase)
            ? "active"
            : null;
    }
}
