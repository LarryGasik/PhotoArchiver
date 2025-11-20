using System;
using System.Globalization;
using LarryGasik.FileSystem;

namespace PhotoArchiver.Logic.Strategies
{
    public class FileNamePhotoDateStrategy : IPhotoDateStrategy
    {
        private static readonly string[] _supportedFormats =
        {
            "yyyyMMdd_HHmmss",
            "yyyyMMddHHmmss"
        };

        public bool CanResolve(FileInformation fileInformation)
        {
            return TryParseDateFromFileName(fileInformation.FileNameWithoutExtension, out _);
        }

        public PhotoDateResolutionResult? Resolve(FileInformation fileInformation)
        {
            if (!TryParseDateFromFileName(fileInformation.FileNameWithoutExtension, out var dateFromFileName))
            {
                return null;
            }

            return new PhotoDateResolutionResult(dateFromFileName, true);
        }

        private static bool TryParseDateFromFileName(string fileNameWithoutExtension, out DateTime date)
        {
            foreach (var format in _supportedFormats)
            {
                if (DateTime.TryParseExact(fileNameWithoutExtension, format, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out date))
                {
                    return true;
                }
            }

            date = default;
            return false;
        }
    }
}
