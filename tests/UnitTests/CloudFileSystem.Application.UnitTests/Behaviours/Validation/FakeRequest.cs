using MediatR;
using System;

namespace CloudFileSystem.Application.UnitTests.Behaviours.Validation;

internal class FakeRequest : IRequest<FakeResponse>
{
    public Guid Id { get; }

    public FakeRequest()
    {
    }

    public FakeRequest(Guid id)
    {
        Id = id;
    }
}