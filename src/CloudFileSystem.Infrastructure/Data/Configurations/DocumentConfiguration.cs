using CloudFileSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudFileSystem.Infrastructure.Data.Configurations;

/// <summary>
/// The configuration of a <see cref="Document"/>.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration&lt;CloudFileSystem.Domain.Entities.Document&gt;"/>
internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    /// <summary>
    /// Configures the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Configure(EntityTypeBuilder<Document> entity)
    {
        entity.ToTable($"{nameof(Document)}s");

        entity.Property(e => e.Id)
            .HasColumnName("Id")
            .HasDefaultValueSql("newsequentialid()");
    }
}