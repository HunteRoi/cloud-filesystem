using FluentValidation;

namespace CloudFileSystem.Application.UnitTests.Behaviours.Validation;

internal class FakeRequestValidator : AbstractValidator<FakeRequest>
{
    public FakeRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}