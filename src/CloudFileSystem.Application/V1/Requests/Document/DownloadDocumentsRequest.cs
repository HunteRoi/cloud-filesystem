using System.Runtime.Serialization;

namespace CloudFileSystem.Application.V1.Requests.Document;

/// <summary>
/// Request sent when willing to download several documents at once
/// </summary>
[DataContract]
public class DownloadDocumentsRequest
{
    /// <summary>
    /// Gets or sets the ids.
    /// </summary>
    /// <value>The ids.</value>
    [DataMember]
    public IEnumerable<Guid> Ids { get; set; }
}