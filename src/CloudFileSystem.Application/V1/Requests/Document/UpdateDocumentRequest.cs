using Microsoft.AspNetCore.Http;
using System;
using System.Runtime.Serialization;

namespace CloudFileSystem.Application.V1.Requests.Document
{
    /// <summary>
    /// Request sent when willing to update a document
    /// </summary>
    [DataContract]
    public class UpdateDocumentRequest
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        internal Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [DataMember]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        [DataMember]
        public IFormFile File { get; set; }

        /// <summary>
        /// Sets the entity identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SetEntityId(Guid id)
        {
            this.Id = id;
        }
    }
}
