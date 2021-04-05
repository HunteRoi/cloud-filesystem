using CloudFileSystem.Core.V1.FileManagement;
using CloudFileSystem.Core.V1.FileManagement.Archive;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace CloudFileSystem.Infrastructure.V1.Archive
{
    /// <summary>
    /// An implementation of the ArchiveManager interface based on ICSharpCode.SharpZipLib
    /// </summary>
    /// <seealso cref="CloudFileSystem.Core.V1.FileManagement.Archive.IArchiveManager" />
    public sealed class SharpZipLibArchiveManager : IArchiveManager
    {
        /// <summary>
        /// The extension
        /// </summary>
        const string EXTENSION = "zip";

        /// <inheritdoc />
        public StorageFile Compress(IEnumerable<StorageFile> files)
        {
            if (files is null) throw new ArgumentNullException(nameof(files));

            // TODO: include folders in the archive
            try
            {
                string archiveName = $"archive_{DateTime.UtcNow:u}.{SharpZipLibArchiveManager.EXTENSION}";
                var stream = new MemoryStream();

                using var zipStream = new ZipOutputStream(stream);
                zipStream.SetLevel(9); // 0-9, 9 being the highest level of compression

                foreach(StorageFile file in files)
                {
                    var buffer = new byte[4096];

                    string entryName = file.FileName ?? file.SafeName;
                    var entry = new ZipEntry(entryName) { DateTime = DateTime.UtcNow };
                    
                    zipStream.PutNextEntry(entry);
                    StreamUtils.Copy(file.Content, zipStream, buffer);
                    zipStream.CloseEntry();
                }

                zipStream.IsStreamOwner = false; // prevent disposal of zipStream to close the underlying stream "stream"
                stream.Position = 0; // reset position of pointer on "stream" to beginning

                return new StorageFile(stream, MimeTypeMap.GetMimeType($".{SharpZipLibArchiveManager.EXTENSION}"), archiveName, archiveName);
            } 
            catch (Exception exception)
            {
                throw new ArchiveException(exception.Message, exception);
            }
        }
    }
}
