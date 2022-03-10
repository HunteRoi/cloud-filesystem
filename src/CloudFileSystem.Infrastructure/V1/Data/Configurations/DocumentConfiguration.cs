using CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudFileSystem.Infrastructure.V1.Data.Configurations;

/// <summary>
/// The Documents table configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{CloudFileSystem.Domain.V1.Aggregates.DocumentAggregate.Document}" />
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    /// <summary>
    /// Configures the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Configure(EntityTypeBuilder<Document> entity)
    {
        entity.ToTable("Documents");

        entity.Property(e => e.Id).HasDefaultValue("newid()");

        entity.Property(e => e.CreatedAt)
            .HasColumnType("datetime2")
            .HasDefaultValue("getdate()");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(255)
            .IsFixedLength();

        entity.Property(e => e.LastModifiedAt)
            .HasColumnType("datetime2");

        entity.Property(e => e.LastModifiedBy)
            .HasMaxLength(255)
            .IsFixedLength();

        entity.Property(e => e.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255)
            .IsFixedLength();

        entity.Property(e => e.Extension)
            .HasMaxLength(5)
            .IsFixedLength();

        entity.Property(e => e.IsFolder)
            .HasDefaultValue("((0))");

        entity.HasOne(e => e.Parent)
            .WithMany(p => p.Documents)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Document_Document");
    }
}