using CloudFileSystem.Core.V1.FileManagement;
using MediatR;
using System;
using System.Collections.Generic;

namespace CloudFileSystem.Domain.V1.Queries.Document
{
    /// <summary>
    /// A query to download several documents
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CloudFileSystem.Core.V1.FileManagement.StorageFile}" />
    public class DownloadDocumentsQuery : IRequest<StorageFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadDocumentsQuery"/> class.
        /// </summary>
        /// <param name="ids">The ids.</param>
        public DownloadDocumentsQuery(IEnumerable<Guid> ids)
        {
            this.Ids = ids;
        }

        /// <summary>
        /// Gets the ids.
        /// </summary>
        /// <value>
        /// The ids.
        /// </value>
        public IEnumerable<Guid> Ids { get; }
    }
}
