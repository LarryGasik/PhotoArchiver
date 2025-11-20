using System.Collections.Generic;
using MetadataExtractor;

namespace PhotoArchiver.Logic
{
    public interface IImageMetadataReader
    {
        IReadOnlyList<Directory> ReadMetadata(string filePath);
    }
}
