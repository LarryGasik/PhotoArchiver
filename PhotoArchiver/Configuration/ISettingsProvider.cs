using System;
using System.IO;
using System.Text.Json;

namespace PhotoArchiver.Configuration
{
    public interface ISettingsProvider
    {
        ArchiveSettings GetSettings(string[] args);
    }

    public class SettingsProvider : ISettingsProvider
    {
        private const string ConfigFileName = "appsettings.json";

        public ArchiveSettings GetSettings(string[] args)
        {
            if (HasCommandLineArguments(args))
            {
                return new ArchiveSettings(args[0], args[1]);
            }

            var configuration = ReadConfiguration();
            ValidateConfiguration(configuration);
            return new ArchiveSettings(configuration.SourceDirectory!, configuration.DestinationDirectory!);
        }

        private static bool HasCommandLineArguments(string[] args)
        {
            return args is { Length: >= 2 }
                && !string.IsNullOrWhiteSpace(args[0])
                && !string.IsNullOrWhiteSpace(args[1]);
        }

        private static AppConfiguration ReadConfiguration()
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, ConfigFileName);

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            var fileContents = File.ReadAllText(configPath);
            var configuration = JsonSerializer.Deserialize<AppConfiguration>(fileContents);

            if (configuration is null)
            {
                throw new InvalidOperationException("Unable to read configuration file.");
            }

            return configuration;
        }

        private static void ValidateConfiguration(AppConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.SourceDirectory) || string.IsNullOrWhiteSpace(configuration.DestinationDirectory))
            {
                throw new InvalidOperationException("SourceDirectory and DestinationDirectory must be configured.");
            }
        }

        private class AppConfiguration
        {
            public string? SourceDirectory { get; set; }
            public string? DestinationDirectory { get; set; }
        }
    }
}
