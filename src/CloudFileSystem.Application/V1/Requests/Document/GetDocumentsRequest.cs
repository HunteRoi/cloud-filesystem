namespace CloudFileSystem.Application.V1.Requests.Document;

/// <summary>
/// A request to get documents
/// </summary>
public class GetDocumentsRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsRequest" /> class.
    /// </summary>
    /// <param name="parentId">The parent identifier.</param>
    public GetDocumentsRequest(Guid? parentId = null)
    {
        this.ParentId = parentId;
    }

    /// <summary>
    /// Gets or sets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    public Guid? ParentId { get; set; }
}