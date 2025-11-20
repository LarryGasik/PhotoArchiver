using System;
using LarryGasik.FileSystem;
using Moq;
using Xunit;

namespace PhotoArchiver.Logic.Tests.FileNameGenerator
{
    public class GenerateUniqueFileName:IDisposable
    {
        private Logic.FileNameGenerator _sut;
        private Mock<IFileOperations> _fileOperations;
        private Mock<IPhotoDateResolver> _photoDateResolver;
        private PhotoDateResolutionResult _photoDate;

        //Setup
        public GenerateUniqueFileName()
        {
            _fileOperations =new Mock<IFileOperations>();
            _photoDateResolver = new Mock<IPhotoDateResolver>();
            _photoDate = new PhotoDateResolutionResult(new DateTime(2014, 02, 02, 12, 30, 30), true);
            
        }

        //Teardown
        public void Dispose()
        {
            _sut = null;
            _fileOperations = null;
            _photoDateResolver = null;
        }

        [Fact]
        public void ShouldPopulateDestinationName()
        {
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"))
                .Returns(false);
            _photoDateResolver.Setup(x => x.GetPhotoDate(It.IsAny<FileInformation>())).Returns(_photoDate);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object, _photoDateResolver.Object);

            //Arrange
            FileInformation fileInformation 
                = new FileInformation(@"d:\path\sub\20140202_123030.jpg");
            string destination = @"c:\destination\";
            
            //Act
            var result = _sut.GenerateFullyQualifiedName(fileInformation, destination);
            
            //Assert
            Assert.NotNull(result.DestinationFileName);
        }

        [Fact]
        public void WhenFileDoesNotExistInArchive()
        {
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"))
                .Returns(false);
            _photoDateResolver.Setup(x => x.GetPhotoDate(It.IsAny<FileInformation>())).Returns(_photoDate);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object, _photoDateResolver.Object);

            //Arrange
            FileInformation fileInformation
                = new FileInformation(@"d:\path\sub\20140202_123030.jpg");
            string destination = @"c:\destination\";

            //Act
            var result = _sut.GenerateFullyQualifiedName(fileInformation, destination);

            //Assert
            Assert.Equal(@"c:\destination\2014-02-28\20140202_123030.jpg", result.FullyQualifiedDestinationName);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"), Times.Once);
        }

        [Fact]
        public void WhenFileDoesExistInArchive()
        {
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"))
                .Returns(true);
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_1.jpg"))
                .Returns(false);
            _photoDateResolver.Setup(x => x.GetPhotoDate(It.IsAny<FileInformation>())).Returns(_photoDate);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object, _photoDateResolver.Object);

            //Arrange
            FileInformation fileInformation
                = new FileInformation(@"d:\path\sub\20140202_123030.jpg");
            string destination = @"c:\destination\";

            //Act
            var result = _sut.GenerateFullyQualifiedName(fileInformation, destination);

            //Assert
            Assert.Equal(@"c:\destination\2014-02-28\20140202_123030_1.jpg", result.FullyQualifiedDestinationName);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"), Times.Once);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_1.jpg"), Times.Once);
        }

        [Fact]
        public void WhenFileDoesExistTwiceInArchive()
        {
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"))
                .Returns(true);
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_1.jpg"))
                .Returns(true);
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_2.jpg"))
                .Returns(false);
            _photoDateResolver.Setup(x => x.GetPhotoDate(It.IsAny<FileInformation>())).Returns(_photoDate);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object, _photoDateResolver.Object);
        
            //Arrange
            FileInformation fileInformation
                = new FileInformation(@"d:\path\sub\20140202_123030.jpg");
            string destination = @"c:\destination\";

            //Act
            var result = _sut.GenerateFullyQualifiedName(fileInformation, destination);

            //Assert
            Assert.Equal(@"c:\destination\2014-02-28\20140202_123030_2.jpg", result.FullyQualifiedDestinationName);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"), Times.Once);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_1.jpg"), Times.Once);
            _fileOperations.Verify(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030_2.jpg"), Times.Once);
        }

        [Fact]
        public void GeneratesNameFromMetadataWhenFileNameMissingTimestamp()
        {
            var metadataDate = new PhotoDateResolutionResult(new DateTime(2020, 03, 04, 05, 06, 07), false);
            _photoDateResolver.Setup(x => x.GetPhotoDate(It.IsAny<FileInformation>())).Returns(metadataDate);
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2020-03-31\20200304_050607.jpg"))
                .Returns(false);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object, _photoDateResolver.Object);

            FileInformation fileInformation
                = new FileInformation(@"d:\path\sub\IMG_1234.jpg");
            string destination = @"c:\destination\";

            var result = _sut.GenerateFullyQualifiedName(fileInformation, destination);

            Assert.Equal(@"c:\destination\2020-03-31\20200304_050607.jpg", result.FullyQualifiedDestinationName);
            Assert.Equal(@"c:\destination\2020-03-31\", result.DestinationPath);
        }
    }
}
