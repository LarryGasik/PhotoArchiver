using System;
using Xunit;

namespace PhotoArchiver.Logic.Tests
{
    public class AlbumNameGenerator:IDisposable
    {
        //Setup
        public AlbumNameGenerator()
        {

        }

        //Teardown
        public void Dispose()
        {
        }

        [Theory]
        [InlineData("20180403_170837.jpg", "2018-04-30")]
        [InlineData("20190403_170837.jpg", "2019-04-30")]
        [InlineData("20191203_170137.jpg", "2019-12-31")]
        [InlineData("20191203_170137.mpg", "2019-12-31")]
        [InlineData("20190203_170137.mpg", "2019-02-28")]
        [InlineData("20200220_170137.mpg", "2020-02-29")]
        public void GenerateAlbumName(string filename, string expected)
        {
            var result = Logic.AlbumNameGenerator.GenerateAlbumName(filename);
            Assert.Equal(expected, result);
        }
    }
}
