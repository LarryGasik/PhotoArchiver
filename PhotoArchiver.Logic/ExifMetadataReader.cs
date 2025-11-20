using System.Collections.Generic;
using MetadataExtractor;

namespace PhotoArchiver.Logic
{
    public class ExifMetadataReader : IImageMetadataReader
    {
        public IReadOnlyList<Directory> ReadMetadata(string filePath)
        {
            return ImageMetadataReader.ReadMetadata(filePath);
        }
    }
}
