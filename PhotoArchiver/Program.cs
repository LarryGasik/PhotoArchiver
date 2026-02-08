using System;
using PhotoArchiver.Configuration;
using PhotoArchiver.Logic;

namespace PhotoArchiver
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run(args, new SettingsProvider(), new ArchiveProcess());
        }

        public static void Run(string[] args, ISettingsProvider settingsProvider, IArchiveProcess archiveProcess, bool waitForUserInput = true)
        {
            if (settingsProvider == null)
            {
                throw new ArgumentNullException(nameof(settingsProvider));
            }

            if (archiveProcess == null)
            {
                throw new ArgumentNullException(nameof(archiveProcess));
            }

            ArchiveSettings settings = settingsProvider.GetSettings(args);

            archiveProcess.ArchivePhotosBasedOnDays(settings.SourceDirectory, settings.DestinationDirectory, false);

            Console.Write("Done");

            if (waitForUserInput)
            {
                Console.ReadLine();
            }
        }
    }
}
