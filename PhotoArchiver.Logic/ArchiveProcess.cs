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

        public ArchiveProcess():this(new FileOperations())
        {

        }

        public ArchiveProcess(IFileOperations fileOperations)
        {
            _fileOperations = fileOperations;
        }

        public void ArchivePhotosBasedOnDays(string sourceDirectory, string destinationDirectory, bool cleanUpSource = false)
        {
            var filePaths = _fileOperations.GetFilesInDirectory(sourceDirectory);
            var fng = new FileNameGenerator(_fileOperations);
            var populatedFileInformation = new List<FileInformation>();

            foreach (var fileInformation in filePaths)
            {
                populatedFileInformation.Add(fng.GenerateFullyQualifiedName(fileInformation, destinationDirectory));
            }
            _fileOperations.CopyFilesToDirectory(populatedFileInformation, false);
            
        }
    }
}
