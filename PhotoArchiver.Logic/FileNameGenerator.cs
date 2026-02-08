using System.IO;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class FileNameGenerator
    {
        private readonly IFileOperations _fileOperations;

        public FileNameGenerator(IFileOperations fileOperationsObject)
        {
            _fileOperations = fileOperationsObject;
        }

        public FileInformation GenerateFullyQualifiedName(FileInformation fileInformation, string destinationDirectory)
        {
            FileInformation resultingFileInformation = fileInformation;
            resultingFileInformation.Album = AlbumNameGenerator.GenerateAlbumName(fileInformation.FileName);
            var albumPath = Path.Combine(destinationDirectory, fileInformation.Album);
            resultingFileInformation.DestinationPath = albumPath + Path.DirectorySeparatorChar;
            var isUnique = false;
            var counter = 0;
            while (!isUnique)
            {
                var uniqueFileName = Path.Combine(albumPath, GenerateFileNameSequence(fileInformation, counter));

                if (!_fileOperations.DoesFileExist(uniqueFileName))
                {
                    resultingFileInformation.FullyQualifiedDestinationName = uniqueFileName;
                    isUnique = true;
                }

                counter++;
            }

            return resultingFileInformation;
        }

        public string GenerateFileNameSequence(FileInformation fileInformation, int counter)
        {
            if (counter == 0)
                return fileInformation.FileName;

            return $"{fileInformation.FileNameWithoutExtension}_{counter.ToString()}{fileInformation.FileExtension}";
        }
    }
}