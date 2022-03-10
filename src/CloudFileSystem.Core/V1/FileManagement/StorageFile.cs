namespace CloudFileSystem.Core.V1.FileManagement;

/// <summary>
/// The storage file implementation
/// </summary>
public sealed class StorageFile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StorageFile" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="fileName">The name.</param>
    /// <param name="safeName">Name of the safe.</param>
    /// <exception cref="System.ArgumentNullException">content or contentType or name</exception>
    public StorageFile(Stream content, string contentType, string fileName, string safeName = null)
    {
        this.Content = content ?? throw new ArgumentNullException(nameof(content));
        this.ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        this.FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        this.SafeName = safeName;
    }

    /// <summary>
    /// Gets the content.
    /// </summary>
    /// <value>The content.</value>
    public Stream Content { get; }

    /// <summary>
    /// Gets the type of the content.
    /// </summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public string FileName { get; }

    /// <summary>
    /// Gets or sets the safe name of the file.
    /// </summary>
    /// <value>The safe name of the file.</value>
    public string SafeName { get; set; }
}