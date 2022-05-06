namespace CloudFileSystem.Domain.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException()
        : base("Data not found")
    {
    }

    public NotFoundException(Guid id)
        : base($"Data with id {id} not found")
    {
        Id = id;
    }

    public Guid Id { get; }
}