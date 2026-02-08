using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LarryGasik.FileSystem
{
    public class FileOperations:IFileOperations
    {
        public void CopyFilesToDirectory( List<FileInformation> filesToMove, bool deleteOriginal = false)
        {
            foreach (var path in filesToMove.Select(x => x.DestinationPath).Distinct())
            {
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);
            }

            foreach (var someFile in filesToMove)
            {
                File.Copy(someFile.FullyQualifiedSourceName, someFile.FullyQualifiedDestinationName);
            }
        }

        public bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public List<FileInformation> GetFilesInDirectory(string sourceDirectory)
        {
            string[] x = Directory.GetFiles(sourceDirectory);
            return x.ToList().Select(z => new FileInformation(z)).ToList();
        }
    }
}
