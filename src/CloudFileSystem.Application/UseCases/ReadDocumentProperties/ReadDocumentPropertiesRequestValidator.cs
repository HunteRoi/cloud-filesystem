using FluentValidation;

namespace CloudFileSystem.Application.UseCases.ReadDocumentProperties;

public class ReadDocumentPropertiesRequestValidator : AbstractValidator<ReadDocumentPropertiesRequest>
{
    public ReadDocumentPropertiesRequestValidator()
    {
        DocumentIdIsNotNullEmptyOrDefault();
    }

    private void DocumentIdIsNotNullEmptyOrDefault() => RuleFor(request => request.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
}