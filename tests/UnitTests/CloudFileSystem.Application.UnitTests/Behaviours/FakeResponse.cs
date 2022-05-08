using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace CloudFileSystem.Application.UnitTests.Behaviours;

internal record FakeResponse(Guid Id);