using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace CloudFileSystem.Application.UnitTests.Behaviours.Validation;

internal record FakeResponse(Guid Id);