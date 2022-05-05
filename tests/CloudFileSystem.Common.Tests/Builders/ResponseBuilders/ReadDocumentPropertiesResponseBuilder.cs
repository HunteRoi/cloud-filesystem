using CloudFileSystem.Application.UseCases.ReadDocumentProperties;

namespace CloudFileSystem.Common.Builders.ResponseBuilders
{
    public class ReadDocumentPropertiesResponseBuilder
    {
        private const string DefaultDocumentId = "5f9a9d5f-94aa-453a-8139-3b5c0c8c25a0";
        private Guid _documentId;

        public ReadDocumentPropertiesResponseBuilder()
        {
            _documentId = Guid.Parse(DefaultDocumentId);
        }

        public static ReadDocumentPropertiesResponseBuilder ReadDocumentPropertiesResponse()
        {
            return new ReadDocumentPropertiesResponseBuilder();
        }

        public ReadDocumentPropertiesResponse Build()
        {
            return new ReadDocumentPropertiesResponse(_documentId);
        }

        public ReadDocumentPropertiesResponseBuilder DocumentId(Guid documentId)
        {
            _documentId = documentId;
            return this;
        }
    }
}