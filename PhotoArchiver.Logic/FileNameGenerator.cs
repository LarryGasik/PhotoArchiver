using System.IO;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic
{
    public class FileNameGenerator
    {
        private readonly IFileOperations _fileOperations;
        private readonly IPhotoDateResolver _photoDateResolver;

        public FileNameGenerator(IFileOperations fileOperationsObject, IPhotoDateResolver photoDateResolver)
        {
            _fileOperations = fileOperationsObject;
            _photoDateResolver = photoDateResolver;
        }

        public FileInformation GenerateFullyQualifiedName(FileInformation fileInformation, string destinationDirectory)
        {
            FileInformation resultingFileInformation = fileInformation;
            var photoDate = _photoDateResolver.GetPhotoDate(fileInformation);

            resultingFileInformation.DestinationFileName = DetermineDestinationFileName(fileInformation, photoDate);
            resultingFileInformation.Album = AlbumNameGenerator.GenerateAlbumName(photoDate.PhotoDate);
            resultingFileInformation.DestinationPath = destinationDirectory + resultingFileInformation.Album + @"\\";

            bool isUnique = false;
            int counter = 0;
            while (!isUnique)
            {
                string uniqueFileName = resultingFileInformation.DestinationPath + GenerateFileNameSequence(resultingFileInformation, counter);

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
            var destinationFileName = string.IsNullOrEmpty(fileInformation.DestinationFileName)
                ? fileInformation.FileName
                : fileInformation.DestinationFileName;

            var destinationNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationFileName);
            var extension = Path.GetExtension(destinationFileName);

            if (counter == 0)
                return destinationFileName;

            return $"{destinationNameWithoutExtension}_{counter}{extension}";
        }

        private string DetermineDestinationFileName(FileInformation fileInformation, PhotoDateResolutionResult photoDate)
        {
            if (photoDate.DateFromFileName)
            {
                return fileInformation.FileName;
            }

            return $"{photoDate.PhotoDate:yyyyMMdd_HHmmss}{fileInformation.FileExtension}";
        }
    }
}
