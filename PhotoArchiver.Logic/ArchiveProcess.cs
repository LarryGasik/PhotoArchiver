using System.Collections.Generic;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class ArchiveProcess
    {
        private IFileOperations _fileOperations;
        private IDirectoryOperations _directoryOperations;

        public ArchiveProcess():this(new FileOperations())
        {

        }

        public ArchiveProcess(IFileOperations fileOperations)
        {
            _fileOperations = fileOperations;
        }

        public void ArchivePhotosBasedOnDays(string SourceDirectory, string DestinationDirectory, bool cleanUpSource)
        {
            //Todo: Get Files From Source Directory
            List<FileInformation> filePaths = _fileOperations.GetFilesInDirectory(SourceDirectory);
            FileNameGenerator fng = new FileNameGenerator(_fileOperations);
            List<FileInformation> populatedFileInformation = new List<FileInformation>();
            foreach (FileInformation fileInformation in filePaths)
            {
                populatedFileInformation.Add(fng.GenerateFullyQualifiedName(fileInformation, DestinationDirectory));
            }
            _fileOperations.CopyFilesToDirectory(filePaths, false);
            
        }
    }
}
