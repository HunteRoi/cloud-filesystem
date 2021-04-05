using MediatR;
using CloudFileSystem.Core.V1.FileManagement;
using System;

namespace CloudFileSystem.Domain.V1.Queries.Document
{
    /// <summary>
    /// A query to download a document
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CloudFileSystem.Core.V1.FileManagement.StorageFile}" />
    public class DownloadDocumentQuery : IRequest<StorageFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadDocumentQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public DownloadDocumentQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }
    }
}
