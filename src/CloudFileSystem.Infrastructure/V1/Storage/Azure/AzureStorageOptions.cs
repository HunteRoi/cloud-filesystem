namespace CloudFileSystem.Infrastructure.V1.Storage.Azure;

/// <summary>
/// Options for the Azure Storage Account
/// </summary>
public sealed class AzureStorageOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AzureStorageOptions" /> class.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="name">The name.</param>
    public AzureStorageOptions(string connectionString, string name)
    {
        this.ConnectionString = connectionString;
        this.Name = name;
    }

    /// <summary>
    /// Gets the connection string.
    /// </summary>
    /// <value>The connection string.</value>
    public string ConnectionString { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; }
}