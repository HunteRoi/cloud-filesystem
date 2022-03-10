using CloudFileSystem.Core.V1.Aggregates;

namespace CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;

/// <summary>
/// Document entity class
/// </summary>
public class Document : Entity
{
    /// <summary>
    /// Prevents a default instance of the <see cref="Document" /> class from being created.
    /// </summary>
    private Document(IEnumerable<Document> children = null)
    {
        this.Documents = children;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="extension">The extension.</param>
    /// <param name="isFolder">if set to <c>true</c> [is folder].</param>
    /// <param name="parentId">The parent identifier.</param>
    /// <exception cref="System.ArgumentNullException">name</exception>
    protected Document(string name, string extension, bool isFolder, Guid? parentId = null)
        : this(isFolder ? new List<Document>() : null)
    {
        this.IsFolder = isFolder;
        this.Rename(name, extension);
        this.MoveTo(parentId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document" /> class as a file.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="extension">The extension.</param>
    /// <param name="parentId">The parent identifier.</param>
    /// <exception cref="System.ArgumentNullException">extension</exception>
    public Document(string name, string extension, Guid? parentId = null)
        : this(name, extension, false, parentId)
    {
        if (string.IsNullOrWhiteSpace(extension)) throw new ArgumentNullException(nameof(extension));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document" /> class as a folder.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="parentId">The parent identifier.</param>
    public Document(string name, Guid? parentId = null)
        : this(name, null, true, parentId)
    {
    }

    /// <summary>
    /// Gets the related documents.
    /// </summary>
    /// <value>The documents.</value>
    public virtual IEnumerable<Document> Documents { get; private set; }

    /// <summary>
    /// Gets the extension.
    /// </summary>
    /// <value>The extension.</value>
    public string Extension { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is folder.
    /// </summary>
    /// <value><c>true</c> if this instance is folder; otherwise, <c>false</c>.</value>
    public bool IsFolder { get; private set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the parent.
    /// </summary>
    /// <value>The parent.</value>
    public Document Parent { get; private set; }

    /// <summary>
    /// Gets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    public Guid? ParentId { get; private set; }

    /// <summary>
    /// Moves to/within a folder.
    /// </summary>
    /// <param name="parentId">The parent identifier.</param>
    public void MoveTo(Guid? parentId = null)
    {
        this.ParentId = parentId;
    }

    /// <summary>
    /// Renames the specified name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="extension">The extension.</param>
    /// <exception cref="System.ArgumentNullException">name</exception>
    public void Rename(string name, string extension)
    {
        this.Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
        this.Extension = extension;
    }

    /// <inheritdoc />
    public override string ToString() => this.IsFolder ? this.Name : $"{this.Name}.{this.Extension.ToLower()}";
}