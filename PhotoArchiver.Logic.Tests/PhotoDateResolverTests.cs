using System;
using System.Collections.Generic;
using LarryGasik.FileSystem;
using MetadataExtractor.Formats.Exif;
using Moq;
using PhotoArchiver.Logic.Strategies;
using Xunit;

namespace PhotoArchiver.Logic.Tests
{
    public class PhotoDateResolverTests : IDisposable
    {
        private readonly Mock<IPhotoDateStrategy> _primaryStrategy;
        private readonly Mock<IPhotoDateStrategy> _secondaryStrategy;

        public PhotoDateResolverTests()
        {
            _primaryStrategy = new Mock<IPhotoDateStrategy>();
            _secondaryStrategy = new Mock<IPhotoDateStrategy>();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void UsesFirstStrategyThatResolves()
        {
            var expectedDate = new PhotoDateResolutionResult(new DateTime(2024, 01, 02, 03, 04, 05), true);
            _primaryStrategy.Setup(x => x.CanResolve(It.IsAny<FileInformation>())).Returns(true);
            _primaryStrategy.Setup(x => x.Resolve(It.IsAny<FileInformation>())).Returns(expectedDate);

            _secondaryStrategy.Setup(x => x.CanResolve(It.IsAny<FileInformation>())).Returns(true);

            var resolver = new PhotoDateResolver(new List<IPhotoDateStrategy>
            {
                _primaryStrategy.Object,
                _secondaryStrategy.Object
            });

            var result = resolver.GetPhotoDate(new FileInformation("test.jpg"));

            Assert.Equal(expectedDate.PhotoDate, result.PhotoDate);
            _secondaryStrategy.Verify(x => x.Resolve(It.IsAny<FileInformation>()), Times.Never);
        }

        [Fact]
        public void JpegMetadataStrategyReadsExifDate()
        {
            var date = new DateTime(2019, 05, 06, 07, 08, 09);
            var metadataReader = new Mock<IImageMetadataReader>();
            var exifDirectory = new ExifSubIfdDirectory();
            exifDirectory.Set(ExifDirectoryBase.TagDateTimeOriginal, date);
            metadataReader.Setup(x => x.ReadMetadata(It.IsAny<string>()))
                .Returns(new List<MetadataExtractor.Directory> { exifDirectory });

            var strategy = new JpegMetadataPhotoDateStrategy(metadataReader.Object);

            var result = strategy.Resolve(new FileInformation("sample.jpg"));

            Assert.NotNull(result);
            Assert.False(result!.DateFromFileName);
            Assert.Equal(date, result.PhotoDate);
        }
    }
}
