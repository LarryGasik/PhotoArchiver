using System.Collections.Generic;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public interface IArchiveProcess
    {
        void ArchivePhotosBasedOnDays(string SourceDirectory, string DestinationDirectory, bool cleanUpSource);
    }

    public class ArchiveProcess : IArchiveProcess
    {
        private readonly IFileOperations _fileOperations;
        private readonly IPhotoDateResolver _photoDateResolver;

        public ArchiveProcess() : this(new FileOperations())
        {
        }

        public ArchiveProcess(IFileOperations fileOperations)
            : this(fileOperations, CreateDefaultResolver())
        {
        }

        public ArchiveProcess(IFileOperations fileOperations, IPhotoDateResolver photoDateResolver)
        {
            _fileOperations = fileOperations;
            _photoDateResolver = photoDateResolver;
        }

        public void ArchivePhotosBasedOnDays(string SourceDirectory, string DestinationDirectory, bool cleanUpSource)
        {
            List<FileInformation> filePaths = _fileOperations.GetFilesInDirectory(SourceDirectory);
            FileNameGenerator fileNameGenerator = new FileNameGenerator(_fileOperations, _photoDateResolver);
            List<FileInformation> populatedFileInformation = new List<FileInformation>();
            foreach (FileInformation fileInformation in filePaths)
            {
                populatedFileInformation.Add(fileNameGenerator.GenerateFullyQualifiedName(fileInformation, DestinationDirectory));
            }

            _fileOperations.CopyFilesToDirectory(populatedFileInformation, false);
        }

        private static IPhotoDateResolver CreateDefaultResolver()
        {
            return new PhotoDateResolver(new List<IPhotoDateStrategy>
            {
                new Strategies.FileNamePhotoDateStrategy(),
                new Strategies.JpegMetadataPhotoDateStrategy(new ExifMetadataReader()),
                new Strategies.FileSystemPhotoDateStrategy()
            });
        }
    }
}
