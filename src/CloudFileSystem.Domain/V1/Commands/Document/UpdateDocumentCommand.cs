using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Domain.V1.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CloudFileSystem.Domain.V1.Commands.Document
{
    /// <summary>
    /// Command to update a document
    /// </summary>
    /// <seealso cref="MediatR.IRequest{System.Guid}" />
    public class UpdateDocumentCommand : IRequest<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDocumentCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContentType">Type of the file content.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <exception cref="System.ArgumentException">id</exception>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public UpdateDocumentCommand(Guid id, string name, Guid? parentId = null, string fileName = null, string fileContentType = null, Stream fileStream = null)
        {
            this.Id = id != Guid.Empty ? id : throw new ArgumentException(nameof(id));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ParentId = parentId;

            if (!string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(fileContentType) && fileStream != null)
            {
                string extension = fileName.GetExtension();
                if (string.IsNullOrWhiteSpace(extension)) throw new ArgumentNullException(nameof(extension), $"Cannot get the extension of {nameof(fileName)} '{fileName}'");

                this.Extension = extension;
                this.File = new StorageFile(fileStream, fileContentType, fileName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDocumentCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="file">The file.</param>
        /// <exception cref="System.ArgumentException">id</exception>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public UpdateDocumentCommand(Guid id, string name, Guid? parentId = null, IFormFile file = null)
            : this(id, name, parentId, file?.FileName, file?.ContentType, file?.OpenReadStream())
        { }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; }

        /// <summary>
        /// Gets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public Guid? ParentId { get; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public StorageFile File { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is folder.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is folder; otherwise, <c>false</c>.
        /// </value>
        public bool IsFolder => this.File is null;
    }
}
