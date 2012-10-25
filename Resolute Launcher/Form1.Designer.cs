namespace Resolute_Launcher {
    partial class Resolute_Launcher {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.launchButton = new System.Windows.Forms.Button();
            this.passText = new System.Windows.Forms.TextBox();
            this.userText = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.passLabel = new System.Windows.Forms.Label();
            this.consoleButton = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.CheckBox();
            this.snapshotButton = new System.Windows.Forms.RadioButton();
            this.normalButton = new System.Windows.Forms.RadioButton();
            this.forceUpdateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.rememberBox = new System.Windows.Forms.CheckBox();
            this.statusBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // launchButton
            // 
            this.launchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.launchButton.Location = new System.Drawing.Point(662, 504);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(110, 46);
            this.launchButton.TabIndex = 2;
            this.launchButton.Text = "Launch!";
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // passText
            // 
            this.passText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passText.Location = new System.Drawing.Point(556, 530);
            this.passText.Name = "passText";
            this.passText.Size = new System.Drawing.Size(100, 20);
            this.passText.TabIndex = 1;
            this.passText.UseSystemPasswordChar = true;
            // 
            // userText
            // 
            this.userText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userText.Location = new System.Drawing.Point(556, 504);
            this.userText.Name = "userText";
            this.userText.Size = new System.Drawing.Size(100, 20);
            this.userText.TabIndex = 0;
            // 
            // userLabel
            // 
            this.userLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userLabel.Location = new System.Drawing.Point(490, 504);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(60, 20);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "Username:";
            this.userLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // passLabel
            // 
            this.passLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passLabel.Location = new System.Drawing.Point(463, 530);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(87, 20);
            this.passLabel.TabIndex = 4;
            this.passLabel.Text = "Password:";
            this.passLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // consoleButton
            // 
            this.consoleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleButton.AutoSize = true;
            this.consoleButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.consoleButton.Location = new System.Drawing.Point(592, 35);
            this.consoleButton.Name = "consoleButton";
            this.consoleButton.Size = new System.Drawing.Size(64, 17);
            this.consoleButton.TabIndex = 5;
            this.consoleButton.Text = "Console";
            this.consoleButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.consoleButton.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.AutoSize = true;
            this.updateButton.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateButton.Checked = true;
            this.updateButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.updateButton.Location = new System.Drawing.Point(541, 12);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(115, 17);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Check for Updates";
            this.updateButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // snapshotButton
            // 
            this.snapshotButton.AutoSize = true;
            this.snapshotButton.Checked = true;
            this.snapshotButton.Location = new System.Drawing.Point(12, 12);
            this.snapshotButton.Name = "snapshotButton";
            this.snapshotButton.Size = new System.Drawing.Size(70, 17);
            this.snapshotButton.TabIndex = 7;
            this.snapshotButton.TabStop = true;
            this.snapshotButton.Text = "Snapshot";
            this.snapshotButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.snapshotButton.UseVisualStyleBackColor = true;
            this.snapshotButton.CheckedChanged += new System.EventHandler(this.snapshotButton_CheckedChanged);
            // 
            // normalButton
            // 
            this.normalButton.AutoSize = true;
            this.normalButton.Location = new System.Drawing.Point(12, 35);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(58, 17);
            this.normalButton.TabIndex = 8;
            this.normalButton.Text = "Normal";
            this.normalButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.normalButton.UseVisualStyleBackColor = true;
            // 
            // forceUpdateButton
            // 
            this.forceUpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forceUpdateButton.Location = new System.Drawing.Point(662, 12);
            this.forceUpdateButton.Name = "forceUpdateButton";
            this.forceUpdateButton.Size = new System.Drawing.Size(110, 40);
            this.forceUpdateButton.TabIndex = 13;
            this.forceUpdateButton.Text = "Force Update!";
            this.forceUpdateButton.UseVisualStyleBackColor = true;
            this.forceUpdateButton.Click += new System.EventHandler(this.forceUpdateButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(672, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 38);
            this.label1.TabIndex = 14;
            this.label1.Text = "NOTE: Select what version first!";
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(12, 96);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(759, 402);
            this.webBrowser.TabIndex = 15;
            this.webBrowser.Url = new System.Uri("http://www.minecraftwiki.net/wiki/Version_history/Development_versions#Weekly_Sna" +
                    "pshots", System.UriKind.Absolute);
            // 
            // rememberBox
            // 
            this.rememberBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rememberBox.AutoSize = true;
            this.rememberBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rememberBox.Location = new System.Drawing.Point(561, 58);
            this.rememberBox.Name = "rememberBox";
            this.rememberBox.Size = new System.Drawing.Size(95, 17);
            this.rememberBox.TabIndex = 16;
            this.rememberBox.Text = "Remember Me";
            this.rememberBox.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusBar.Location = new System.Drawing.Point(12, 530);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(100, 23);
            this.statusBar.TabIndex = 17;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(118, 533);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(27, 13);
            this.statusLabel.TabIndex = 18;
            this.statusLabel.Text = "N/A";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "About Resolute Launcher";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Resolute_Launcher
            // 
            this.AcceptButton = this.launchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.rememberBox);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.forceUpdateButton);
            this.Controls.Add(this.normalButton);
            this.Controls.Add(this.snapshotButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.consoleButton);
            this.Controls.Add(this.passLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.userText);
            this.Controls.Add(this.passText);
            this.Controls.Add(this.launchButton);
            this.Name = "Resolute_Launcher";
            this.Text = "Resolute Launcher";
            this.Load += new System.EventHandler(this.Resolute_Launcher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.TextBox userText;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passLabel;
        private System.Windows.Forms.CheckBox consoleButton;
        private System.Windows.Forms.CheckBox updateButton;
        private System.Windows.Forms.RadioButton snapshotButton;
        private System.Windows.Forms.RadioButton normalButton;
        private System.Windows.Forms.Button forceUpdateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.CheckBox rememberBox;
        private System.Windows.Forms.ProgressBar statusBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button button1;
    }
}

