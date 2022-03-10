using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Domain.V1.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CloudFileSystem.Domain.V1.Commands.Document;

/// <summary>
/// Command to create a document
/// </summary>
/// <seealso cref="MediatR.IRequest{System.Guid}" />
public class CreateDocumentCommand : IRequest<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDocumentCommand" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="parentId">The parent identifier.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContentType">Type of the file content.</param>
    /// <param name="fileStream">The file stream.</param>
    /// <exception cref="System.ArgumentNullException">name</exception>
    public CreateDocumentCommand(string name, Guid? parentId = null, string fileName = null, string fileContentType = null, Stream fileStream = null)
    {
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
    /// Initializes a new instance of the <see cref="CreateDocumentCommand" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="parentId">The parent identifier.</param>
    /// <param name="file">The file.</param>
    /// <exception cref="System.ArgumentNullException">name</exception>
    public CreateDocumentCommand(string name, Guid? parentId = null, IFormFile file = null)
        : this(name, parentId, file?.FileName, file?.ContentType, file?.OpenReadStream())
    { }

    /// <summary>
    /// Gets the extension.
    /// </summary>
    /// <value>The extension.</value>
    public string Extension { get; }

    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <value>The file.</value>
    public StorageFile File { get; }

    /// <summary>
    /// Gets a value indicating whether this instance represents a folder.
    /// </summary>
    /// <value><c>true</c> if this instance represents a folder; otherwise, <c>false</c>.</value>
    public bool IsFolder => this.File is null;

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; }

    /// <summary>
    /// Gets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    public Guid? ParentId { get; }
}