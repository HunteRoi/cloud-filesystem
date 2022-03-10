using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CloudFileSystem.Core.V1.Responses;

/// <summary>
/// Class ApiResponse.
/// </summary>
/// <typeparam name="T"></typeparam>
[DataContract]
public class ApiResponse<T>
{
    /// <summary>
    /// Prevents a default instance of the <see cref="ApiResponse{T}" /> class from being created.
    /// </summary>
    private ApiResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="success">if set to <c>true</c> [success].</param>
    [JsonConstructor]
    public ApiResponse(T data, bool success)
    {
        this.Data = data;
        this.Success = success;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public ApiResponse(T data) : this(data, true)
    {
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>The data.</value>
    [DataMember]
    public T Data { get; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="ApiResponse{T}" /> is success.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Success { get; }
}