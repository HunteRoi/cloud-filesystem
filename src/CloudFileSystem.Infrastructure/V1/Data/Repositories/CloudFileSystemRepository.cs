using CloudFileSystem.Core.V1.Data.Repositories;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Domain.V1.Interfaces.Data;
using CloudFileSystem.Infrastructure.V1.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudFileSystem.Infrastructure.V1.Data.Repositories
{
    /// <summary>
    /// Main class of the partial repository.
    /// </summary>
    /// <seealso cref="CloudFileSystem.Core.V1.Data.Repositories.Repository" />
    /// <seealso cref="CloudFileSystem.Core.V1.Interfaces.Data.ICloudFileSystemRepository" />
    public partial class CloudFileSystemRepository : Repository, ICloudFileSystemRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly CloudFileSystemDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFileSystemRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CloudFileSystemRepository(CloudFileSystemDbContext context) 
            : base(context)
        {
            this.context = context;
        }
    }
}
