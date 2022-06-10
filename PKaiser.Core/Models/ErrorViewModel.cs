namespace PKaiser.Core.Models;

/// <summary>
/// The class that represents an error view.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Gets or sets the request that threw the error.
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// Gets whether to show the request id or not.
    /// </summary>
    public bool ShowRequestId
        => !string.IsNullOrEmpty(RequestId);
}