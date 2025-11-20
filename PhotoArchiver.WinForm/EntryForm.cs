using System.Diagnostics;
using System.IO;
using PhotoArchiver.Configuration;
using PhotoArchiver.Logic;

namespace PhotoArchiver.WinForm
{
    public partial class EntryForm : Form
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IArchiveProcess _archiveProcess;

        public EntryForm() : this(new SettingsProvider(), new ArchiveProcess())
        {
        }

        public EntryForm(ISettingsProvider settingsProvider, IArchiveProcess archiveProcess)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
            _archiveProcess = archiveProcess ?? throw new ArgumentNullException(nameof(archiveProcess));

            InitializeComponent();
            LoadDefaultSettings();
        }

        private void LoadDefaultSettings()
        {
            try
            {
                var settings = _settingsProvider.GetSettings(Array.Empty<string>());
                sourceTextBox.Text = settings.SourceDirectory;
                destinationTextBox.Text = settings.DestinationDirectory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load default settings: {ex.Message}", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BrowseSourceButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select the source directory",
                SelectedPath = sourceTextBox.Text
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                sourceTextBox.Text = dialog.SelectedPath;
            }
        }

        private void BrowseDestinationButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select the destination directory",
                SelectedPath = destinationTextBox.Text
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                destinationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ArchiveButton_Click(object? sender, EventArgs e)
        {
            archiveButton.Enabled = false;

            try
            {
                _archiveProcess.ArchivePhotosBasedOnDays(sourceTextBox.Text, destinationTextBox.Text, false);
                MessageBox.Show(this, "Archiving complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                openDestinationButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Archiving failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                archiveButton.Enabled = true;
            }
        }

        private void OpenDestinationButton_Click(object? sender, EventArgs e)
        {
            var destinationPath = destinationTextBox.Text;

            if (!Directory.Exists(destinationPath))
            {
                MessageBox.Show(this, "Destination directory does not exist.", "Folder not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = destinationPath,
                UseShellExecute = true
            });
        }
    }
}
