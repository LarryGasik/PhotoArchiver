using System.IO;

namespace LarryGasik.FileSystem
{ 
    public class FileInformation
    {
        public FileInformation(string fullyQualifiedSourcePath)
        {
            FileInfo file = new FileInfo(fullyQualifiedSourcePath);
            FullyQualifiedSourceName = fullyQualifiedSourcePath;
            FileName = file.Name;
            DestinationFileName = file.Name;
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullyQualifiedSourcePath);
            FileExtension = Path.GetExtension(fullyQualifiedSourcePath);
        }

        public string FullyQualifiedSourceName { get; }
        public string FileName { get; }
        public string Album { get; set; }
        public string DestinationFileName { get; set; }
        public string FullyQualifiedDestinationName { get; set; }
        public string FileNameWithoutExtension { get;  }
        public string FileExtension { get; }
        public string DestinationPath { get; set; }
    }
}
