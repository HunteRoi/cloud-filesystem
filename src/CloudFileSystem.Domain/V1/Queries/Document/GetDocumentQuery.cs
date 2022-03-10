using MediatR;

namespace CloudFileSystem.Domain.V1.Queries.Document;

/// <summary>
/// A query to read a document
/// </summary>
/// <seealso cref="MediatR.IRequest{CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document}" />
public class GetDocumentQuery : IRequest<CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentQuery" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public GetDocumentQuery(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; }
}