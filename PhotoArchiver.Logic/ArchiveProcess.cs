using System.Collections.Generic;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class ArchiveProcess
    {
        private readonly IFileOperations _fileOperations;

        public ArchiveProcess():this(new FileOperations())
        {

        }

        public ArchiveProcess(IFileOperations fileOperations)
        {
            _fileOperations = fileOperations;
        }

        public void ArchivePhotosBasedOnDays(string sourceDirectory, string destinationDirectory)
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
