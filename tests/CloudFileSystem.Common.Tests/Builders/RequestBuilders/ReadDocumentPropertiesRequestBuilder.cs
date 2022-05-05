using CloudFileSystem.Application.UseCases.ReadDocumentProperties;

namespace CloudFileSystem.Common.Builders.RequestBuilders
{
    public class ReadDocumentPropertiesRequestBuilder
    {
        private const string DefaultDocumentId = "5f9a9d5f-94aa-453a-8139-3b5c0c8c25a0";
        private Guid _documentId;

        public ReadDocumentPropertiesRequestBuilder()
        {
            _documentId = Guid.Parse(DefaultDocumentId);
        }

        public static ReadDocumentPropertiesRequestBuilder ReadDocumentPropertiesRequest()
        {
            return new ReadDocumentPropertiesRequestBuilder();
        }

        public ReadDocumentPropertiesRequest Build()
        {
            return new ReadDocumentPropertiesRequest(_documentId);
        }

        public ReadDocumentPropertiesRequestBuilder DocumentId(Guid documentId)
        {
            _documentId = documentId;
            return this;
        }
    }
}