namespace CloudFileSystem.Domain;
public sealed record ErrorResponse(string Details, IDictionary<string, string[]>? Errors);