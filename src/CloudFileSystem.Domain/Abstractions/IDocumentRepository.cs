using CloudFileSystem.Domain.Entities;

namespace CloudFileSystem.Application.Abstractions;

public interface IDocumentRepository
{
    Task<Document> GetByDocumentId(Guid documentId);
}