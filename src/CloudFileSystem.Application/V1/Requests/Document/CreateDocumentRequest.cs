using Microsoft.AspNetCore.Http;
using System;
using System.Runtime.Serialization;

namespace CloudFileSystem.Application.V1.Requests.Document
{
    /// <summary>
    /// Request sent when willing to create a new document
    /// </summary>
    [DataContract]
    public class CreateDocumentRequest
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [DataMember]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        [DataMember]
        public IFormFile File { get; set; }
    }
}
