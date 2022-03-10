namespace CloudFileSystem.Domain.V1.Extensions;

/// <summary>
/// An extensions class for <see cref="string" />
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Gets the extension out of a file name.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException">fileName</exception>
    public static string GetExtension(this string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new NullReferenceException(nameof(fileName));

        int dotIndex = fileName.LastIndexOf('.');
        return dotIndex != -1 && fileName.Length > dotIndex + 1
            ? fileName[(dotIndex + 1)..]
            : null;
    }
}