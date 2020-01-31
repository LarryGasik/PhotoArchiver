using System;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class FileNameGenerator
    {
        private IFileOperations _fileOperations;

        public FileNameGenerator(IFileOperations fileOperationsObject)
        {
            _fileOperations = fileOperationsObject;
        }

        public FileInformation GenerateFullyQualifiedName(FileInformation fileInformation, string destinationDirectory)
        {
            FileInformation resultingFileInformation = fileInformation;
            resultingFileInformation.Album = AlbumNameGenerator.GenerateAlbumName(fileInformation.FileName);
            resultingFileInformation.DestinationPath = destinationDirectory + fileInformation.Album + @"\";
            bool isUnique = false;
            int counter = 0;
            while (!isUnique)
            {
                string uniqueFileName = resultingFileInformation.DestinationPath + GenerateFileNameSequence(fileInformation, counter);

                if (_fileOperations.DoesFileExist(uniqueFileName) == false)
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
