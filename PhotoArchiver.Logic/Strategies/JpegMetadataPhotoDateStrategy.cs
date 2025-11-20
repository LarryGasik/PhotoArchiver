using System;
using System.IO;
using System.Linq;
using LarryGasik.FileSystem;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace PhotoArchiver.Logic.Strategies
{
    public class JpegMetadataPhotoDateStrategy : IPhotoDateStrategy
    {
        private readonly IImageMetadataReader _metadataReader;

        public JpegMetadataPhotoDateStrategy(IImageMetadataReader metadataReader)
        {
            _metadataReader = metadataReader;
        }

        public bool CanResolve(FileInformation fileInformation)
        {
            var extension = Path.GetExtension(fileInformation.FileName).ToLowerInvariant();
            return extension is ".jpg" or ".jpeg";
        }

        public PhotoDateResolutionResult? Resolve(FileInformation fileInformation)
        {
            if (!CanResolve(fileInformation))
            {
                return null;
            }

            try
            {
                var directories = _metadataReader.ReadMetadata(fileInformation.FullyQualifiedSourceName);
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

                var date = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal)
                           ?? subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime)
                           ?? ifd0Directory?.GetDateTime(ExifDirectoryBase.TagDateTime);

                return date.HasValue
                    ? new PhotoDateResolutionResult(date.Value, false)
                    : null;
            }
            catch (ImageProcessingException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}
