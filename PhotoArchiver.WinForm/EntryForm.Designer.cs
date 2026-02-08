namespace PhotoArchiver.WinForm
{
    partial class EntryForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sourceLabel = new Label();
            sourceTextBox = new TextBox();
            browseSourceButton = new Button();
            destinationLabel = new Label();
            destinationTextBox = new TextBox();
            browseDestinationButton = new Button();
            archiveButton = new Button();
            openDestinationButton = new Button();
            SuspendLayout();
            // 
            // sourceLabel
            // 
            sourceLabel.AutoSize = true;
            sourceLabel.Location = new Point(23, 23);
            sourceLabel.Name = "sourceLabel";
            sourceLabel.Size = new Size(95, 15);
            sourceLabel.TabIndex = 0;
            sourceLabel.Text = "Source directory";
            // 
            // sourceTextBox
            // 
            sourceTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sourceTextBox.Location = new Point(23, 41);
            sourceTextBox.Name = "sourceTextBox";
            sourceTextBox.Size = new Size(435, 23);
            sourceTextBox.TabIndex = 1;
            // 
            // browseSourceButton
            // 
            browseSourceButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseSourceButton.Location = new Point(464, 41);
            browseSourceButton.Name = "browseSourceButton";
            browseSourceButton.Size = new Size(94, 23);
            browseSourceButton.TabIndex = 2;
            browseSourceButton.Text = "Browse";
            browseSourceButton.UseVisualStyleBackColor = true;
            browseSourceButton.Click += BrowseSourceButton_Click;
            // 
            // destinationLabel
            // 
            destinationLabel.AutoSize = true;
            destinationLabel.Location = new Point(23, 82);
            destinationLabel.Name = "destinationLabel";
            destinationLabel.Size = new Size(116, 15);
            destinationLabel.TabIndex = 3;
            destinationLabel.Text = "Destination directory";
            // 
            // destinationTextBox
            // 
            destinationTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            destinationTextBox.Location = new Point(23, 100);
            destinationTextBox.Name = "destinationTextBox";
            destinationTextBox.Size = new Size(435, 23);
            destinationTextBox.TabIndex = 4;
            // 
            // browseDestinationButton
            // 
            browseDestinationButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseDestinationButton.Location = new Point(464, 100);
            browseDestinationButton.Name = "browseDestinationButton";
            browseDestinationButton.Size = new Size(94, 23);
            browseDestinationButton.TabIndex = 5;
            browseDestinationButton.Text = "Browse";
            browseDestinationButton.UseVisualStyleBackColor = true;
            browseDestinationButton.Click += BrowseDestinationButton_Click;
            // 
            // archiveButton
            // 
            archiveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            archiveButton.Location = new Point(464, 147);
            archiveButton.Name = "archiveButton";
            archiveButton.Size = new Size(94, 27);
            archiveButton.TabIndex = 6;
            archiveButton.Text = "Archive";
            archiveButton.UseVisualStyleBackColor = true;
            archiveButton.Click += ArchiveButton_Click;
            // 
            // openDestinationButton
            // 
            openDestinationButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            openDestinationButton.Enabled = false;
            openDestinationButton.Location = new Point(23, 147);
            openDestinationButton.Name = "openDestinationButton";
            openDestinationButton.Size = new Size(192, 27);
            openDestinationButton.TabIndex = 7;
            openDestinationButton.Text = "Open destination folder";
            openDestinationButton.UseVisualStyleBackColor = true;
            openDestinationButton.Click += OpenDestinationButton_Click;
            // 
            // EntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 199);
            Controls.Add(openDestinationButton);
            Controls.Add(archiveButton);
            Controls.Add(browseDestinationButton);
            Controls.Add(destinationTextBox);
            Controls.Add(destinationLabel);
            Controls.Add(browseSourceButton);
            Controls.Add(sourceTextBox);
            Controls.Add(sourceLabel);
            MinimumSize = new Size(601, 238);
            Name = "EntryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Photo Archiver";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label sourceLabel;
        private TextBox sourceTextBox;
        private Button browseSourceButton;
        private Label destinationLabel;
        private TextBox destinationTextBox;
        private Button browseDestinationButton;
        private Button archiveButton;
        private Button openDestinationButton;
    }
}
