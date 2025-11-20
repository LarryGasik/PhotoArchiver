using System;
using System.IO;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic.Strategies
{
    public class FileSystemPhotoDateStrategy : IPhotoDateStrategy
    {
        public bool CanResolve(FileInformation fileInformation)
        {
            return File.Exists(fileInformation.FullyQualifiedSourceName);
        }

        public PhotoDateResolutionResult? Resolve(FileInformation fileInformation)
        {
            if (!CanResolve(fileInformation))
            {
                return null;
            }

            var creationTime = File.GetCreationTime(fileInformation.FullyQualifiedSourceName);
            return new PhotoDateResolutionResult(creationTime, false);
        }
    }
}
