using System.Threading.Tasks;

namespace CloudFileSystem.Core.V1.FileManagement.Storage
{
    /// <summary>
    /// The storage manager interface
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// Downloads the asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Task<StorageFile> DownloadAsync(string fileName);

        /// <summary>
        /// Uploads the asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns></returns>
        Task UploadAsync(StorageFile file, bool overwrite = false);
    }
}
