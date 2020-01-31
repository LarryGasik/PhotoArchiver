using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LarryGasik.FileSystem
{
    public interface IFileOperations
    {
        void CopyFilesToDirectory(List<FileInformation> FilesToMove, bool DeleteOriginal);
        bool DoesFileExist(string filePath);
        List<FileInformation> GetFilesInDirectory(string sourceDirectory);
    }
}
