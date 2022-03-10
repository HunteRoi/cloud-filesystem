namespace CloudFileSystem.Core.V1.FileManagement.Archive;

/// <summary>
/// The archive manager interface
/// </summary>
public interface IArchiveManager
{
    /// <summary>
    /// Compresses the specified files into an archive file.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">files</exception>
    /// <exception cref="CloudFileSystem.Core.V1.FileManagement.Archive.ArchiveException"></exception>
    public StorageFile Compress(IEnumerable<StorageFile> files);
}