using System;
using Moq;
using PhotoArchiver.Configuration;
using PhotoArchiver.Logic;
using Xunit;

namespace PhotoArchiver.Tests
{
    public class ProgramTests
    {
        
        //[Fact]
        //public void Run_UsesSettingsProviderAndArchiveProcess()
        //{
        //    var expectedSettings = new ArchiveSettings("C:/Source", "D:/Destination");
        //    var settingsProvider = new Mock<ISettingsProvider>();
        //    settingsProvider
        //        .Setup(provider => provider.GetSettings(It.IsAny<string[]>()))
        //        .Returns(expectedSettings);

        //    var archiveProcess = new Mock<IArchiveProcess>();
        //    string[] args = Array.Empty<string>();

        //    Program.Run(args, settingsProvider.Object, archiveProcess.Object, waitForUserInput: false);

        //    settingsProvider.Verify(provider => provider.GetSettings(args), Times.Once);
        //    archiveProcess.Verify(process => process.ArchivePhotosBasedOnDays(expectedSettings.SourceDirectory, expectedSettings.DestinationDirectory, false), Times.Once);
        //}
    }
}
