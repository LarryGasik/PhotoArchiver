using LarryGasik.FileSystem;
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
            Logic.FileNameGenerator _sut = new Logic.FileNameGenerator(new FileOperations());
            string result = _sut.GenerateFileNameSequence(fileInfo, counter);
            Assert.Equal(expected, result);

        }
    }
}
