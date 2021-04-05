using Azure;
using Azure.Storage.Files.Shares;
using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Core.V1.FileManagement.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudFileSystem.Infrastructure.V1.Storage.Azure
{
    /// <summary>
    /// A storage manager implementation for Azure File Share
    /// </summary>
    /// <seealso cref="CloudFileSystem.Core.V1.FileManagement.Storage.IStorageManager" />
    public sealed class AzureFileShareStorageManager : IStorageManager
    {
        /// <summary>
        /// The share client
        /// </summary>
        private readonly ShareClient _shareClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFileShareStorageManager"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AzureFileShareStorageManager(AzureStorageOptions options)
        {
            this._shareClient = new ShareClient(options.ConnectionString, options.Name);
        }

        /// <summary>
        /// Removes the invalid characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        private string RemoveInvalidCharacters(string str)
        {
            return string.Concat(str.Split(Path.GetInvalidFileNameChars()));
        }

        /// <summary>
        /// Downloads the asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">fileName</exception>
        /// <exception cref="CloudFileSystem.Core.V1.FileManagement.Storage.StorageException">
        /// shareExists - DownloadAsync
        /// or
        /// rootDirectoryExists - DownloadAsync
        /// or
        /// fileExists - DownloadAsync
        /// or
        /// downloadResponse - DownloadAsync
        /// </exception>
        public async Task<StorageFile> DownloadAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

            bool shareExists = await this._shareClient.ExistsAsync();
            if (!shareExists) throw new StorageException(nameof(shareExists), nameof(this.DownloadAsync));

            ShareDirectoryClient rootDirectory = this._shareClient.GetRootDirectoryClient();
            bool rootDirectoryExists = await rootDirectory.ExistsAsync();
            if (!rootDirectoryExists) throw new StorageException(nameof(rootDirectoryExists), nameof(this.DownloadAsync));

            string safeName = this.RemoveInvalidCharacters(fileName);
            ShareFileClient file = rootDirectory.GetFileClient(safeName);
            bool fileExists = await file.ExistsAsync();
            if (!fileExists) throw new StorageException(nameof(fileExists), nameof(this.DownloadAsync));

            var downloadResponse = await file.DownloadAsync();
            if (downloadResponse is null || downloadResponse.Value is null) throw new StorageException(nameof(downloadResponse), nameof(this.DownloadAsync));

            return new StorageFile(downloadResponse.Value.Content, downloadResponse.Value.ContentType, fileName, safeName);
        }

        /// <summary>
        /// Uploads the asynchronous.
        /// </summary>
        /// <param name="storageFile">The storage file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <exception cref="System.ArgumentNullException">storageFile</exception>
        /// <exception cref="System.ArgumentException">storageFile</exception>
        /// <exception cref="CloudFileSystem.Core.V1.FileManagement.Storage.StorageException">
        /// rootDirectoryExists - UploadAsync
        /// or
        /// fileExists - UploadAsync
        /// or
        /// Path - UploadAsync
        /// </exception>
        public async Task UploadAsync(StorageFile storageFile, bool overwrite = false)
        {
            if (storageFile is null) throw new ArgumentNullException(nameof(storageFile));
            if (string.IsNullOrWhiteSpace(storageFile.SafeName) && string.IsNullOrWhiteSpace(storageFile.FileName)) throw new ArgumentException(nameof(storageFile));

            ShareDirectoryClient rootDirectory = this._shareClient.GetRootDirectoryClient();
            bool rootDirectoryExists = await rootDirectory.ExistsAsync();
            if (!rootDirectoryExists) throw new StorageException(nameof(rootDirectoryExists), nameof(this.UploadAsync));

            string safeName = storageFile.SafeName ?? this.RemoveInvalidCharacters(storageFile.FileName);
            ShareFileClient file = rootDirectory.GetFileClient(safeName);
            bool fileExists = await file.ExistsAsync();
            if (fileExists && !overwrite) throw new StorageException(nameof(fileExists), nameof(this.UploadAsync));

            await file.CreateAsync(storageFile.Content.Length);
            using var reader = new BinaryReader(storageFile.Content);
            int blockSize = 1 * 1024 * 1024; // 1MB
            long offset = 0;
            while (true)
            {
                byte[] buffer = reader.ReadBytes(blockSize);
                if (buffer.Length == 0) break;

                using var uploadChunk = new MemoryStream();
                uploadChunk.Write(buffer, 0, buffer.Length);
                uploadChunk.Position = 0;

                var httpRange = new HttpRange(offset, buffer.Length);
                await file.UploadRangeAsync(httpRange, uploadChunk);

                offset += buffer.Length;
            }

            if (string.IsNullOrWhiteSpace(file.Path)) throw new StorageException(nameof(file.Path), nameof(this.UploadAsync));
        }
    }
}
 