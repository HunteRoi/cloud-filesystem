using MediatR;

namespace CloudFileSystem.Domain.V1.Queries.Document;

/// <summary>
/// A query to read several documents.
/// </summary>
/// <seealso cref="MediatR.IRequest{System.Collections.Generic.IEnumerable{CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document}}" />
public class GetDocumentsQuery : IRequest<IEnumerable<CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsQuery" /> class.
    /// </summary>
    /// <param name="parentId">The parent identifier.</param>
    public GetDocumentsQuery(Guid? parentId = null)
    {
        this.ParentId = parentId;
    }

    /// <summary>
    /// Gets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    public Guid? ParentId { get; }
}