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
        [InlineData("2018-04-03", "2018-04-30")]
        [InlineData("2019-04-03", "2019-04-30")]
        [InlineData("2019-12-03", "2019-12-31")]
        [InlineData("2019-02-03", "2019-02-28")]
        [InlineData("2020-02-20", "2020-02-29")]
        public void GenerateAlbumName(string dateValue, string expected)
        {
            var result = Logic.AlbumNameGenerator.GenerateAlbumName(DateTime.Parse(dateValue));
            Assert.Equal(expected, result);
        }
    }
}
