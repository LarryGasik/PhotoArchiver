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

        //Setup
        public GenerateUniqueFileName()
        {
            _fileOperations =new Mock<IFileOperations>();
            
        }

        //Teardown
        public void Dispose()
        {
            _sut = null;
            _fileOperations = null;
        }

        [Fact]
        public void ShouldPopulateDestinationName()
        {
            _fileOperations.Setup(x => x.DoesFileExist(@"c:\destination\2014-02-28\20140202_123030.jpg"))
                .Returns(false);
            _sut = new Logic.FileNameGenerator(_fileOperations.Object);

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
            _sut = new Logic.FileNameGenerator(_fileOperations.Object);

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
            _sut = new Logic.FileNameGenerator(_fileOperations.Object);

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
            _sut = new Logic.FileNameGenerator(_fileOperations.Object);

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
    }
}
