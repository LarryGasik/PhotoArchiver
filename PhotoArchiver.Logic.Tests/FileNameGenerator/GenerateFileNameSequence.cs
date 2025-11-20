using LarryGasik.FileSystem;
using Moq;
using Xunit;

namespace PhotoArchiver.Logic.Tests.FileNameGenerator
{
    public class GenerateFileNameSequence
    {
        [Theory]
            [InlineData("C:\\file.jpg", 0, "file.jpg")]
            [InlineData("C:\\album\\file.jpg", 1, "file_1.jpg")]
            [InlineData("C:\\file.jpg", 2, "file_2.jpg")]
            public void GenerateFileNameSequenceTest(string filename, int counter, string expected)
            {
                FileInformation fileInfo = new FileInformation(filename);
                var photoDateResolver = new Mock<IPhotoDateResolver>();
                Logic.FileNameGenerator _sut = new Logic.FileNameGenerator(new FileOperations(), photoDateResolver.Object);
                string result = _sut.GenerateFileNameSequence(fileInfo, counter);
                Assert.Equal(expected, result);

        }
    }
}
