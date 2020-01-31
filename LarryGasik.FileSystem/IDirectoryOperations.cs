using System;
using System.Collections.Generic;

namespace LarryGasik.FileSystem
{
    public interface IDirectoryOperations
    {
        List<string> GetFilesInDirectory();
        void CreateDirectoryIfNotExisting();
    }
}
