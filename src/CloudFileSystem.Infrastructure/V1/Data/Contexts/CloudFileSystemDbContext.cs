using CloudFileSystem.Core.V1.Data.Contexts;
using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using CloudFileSystem.Infrastructure.V1.Data.Configurations;
using CloudFileSystem.Infrastructure.V1.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CloudFileSystem.Infrastructure.V1.Data.Contexts
{
    /// <summary>
    /// The main data context.
    /// </summary>
    /// <seealso cref="CloudFileSystem.Core.V1.Data.Contexts.DataContext" />
    /// <seealso cref="CloudFileSystem.Infrastructure.V1.Data.Interfaces.ICloudFileSystemDataContext" />
    public class CloudFileSystemDbContext : DataContext, ICloudFileSystemDataContext
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFileSystemDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="configuration">The configuration.</param>
        public CloudFileSystemDbContext(DbContextOptions<CloudFileSystemDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this._connectionString = configuration.GetConnectionString("Current");
        }

        /// <inheritdoc />
        public DbSet<Document> Documents { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
            optionsBuilder.EnableDetailedErrors();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        }
    }
}
