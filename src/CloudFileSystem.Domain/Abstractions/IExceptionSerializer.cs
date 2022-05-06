namespace CloudFileSystem.Domain.Abstractions;

public interface IExceptionSerializer
{
    string SerializeException(Exception exception);
}