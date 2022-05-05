using FluentValidation;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

internal class ReadDocumentPropertiesRequestValidator : AbstractValidator<ReadDocumentPropertiesRequest>
{
    public ReadDocumentPropertiesRequestValidator()
    {
        DocumentIdIsNotDefault();
    }

    private void DocumentIdIsNotDefault() => RuleFor(request => request.DocumentId).NotEmpty();
}