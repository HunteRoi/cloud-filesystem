using System.Runtime.Serialization;

namespace CloudFileSystem.Application.V1.Responses.Document;

/// <summary>
/// Response sent when requesting a Document.
/// </summary>
[DataContract]
public class DocumentResponse
{
    /// <summary>
    /// Gets or sets the extension.
    /// </summary>
    /// <value>The extension.</value>
    [DataMember]
    public string Extension { get; set; }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is folder.
    /// </summary>
    /// <value><c>true</c> if this instance is folder; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsFolder { get; set; }

    /// <summary>
    /// Gets or sets the last modified at.
    /// </summary>
    /// <value>The last modified at.</value>
    [DataMember]
    public DateTime? LastModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the last modified by.
    /// </summary>
    /// <value>The last modified by.</value>
    [DataMember]
    public string LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    [DataMember]
    public Guid ParentId { get; set; }
}