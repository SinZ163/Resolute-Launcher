using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace Resolute_Launcher {
    public partial class Resolute_Launcher : Form {

        public String username;
        public String sessionID;

        public Boolean allowOffline;

        public rememberMe rememberMe;

        //public int completed = 1;

        public String link = "";

        public String path;

        public String rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".resolute/");

        /*
        \\ ==================================================== //
                                  Misc
        // ==================================================== \\
        */
        private void Resolute_Launcher_Load(object sender, EventArgs e) {
            rememberMe = new rememberMe(this);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            rememberMe.remember();
        }

        private void forceUpdateButton_Click(object sender, EventArgs e) {
            if (snapshotButton.Checked == true) {
                Directory.Delete(Path.Combine(rootPath, "snapshot/.minecraft/bin/"), true);
            }
            else if (normalButton.Checked == true) {
                Directory.Delete(Path.Combine(rootPath, "normal/.minecraft/bin/"), true);
            }
        }

        private void snapshotButton_CheckedChanged(object sender, EventArgs e) {
            if (snapshotButton.Checked == true) {
                webBrowser.Url = new Uri("http://www.minecraftwiki.net/wiki/Version_history/Development_versions#Weekly_Snapshots");
            }
            else if (normalButton.Checked == true) {
                webBrowser.Url = new Uri("http://mcupdate.tumblr.com");
            }
        }

        public Resolute_Launcher() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            aboutResolute about = new aboutResolute();
            about.Show();
        }

        private void launchButton_Click(object sender, EventArgs e) {
            int gamemode = 0;
            if (normalButton.Checked) {
                gamemode = 0;
            }
            else if (snapshotButton.Checked) {
                gamemode = 1;
            }
            Login login = new Login(this);
            login.Init(userText.Text, passText.Text, gamemode, updateButton.Checked, rootPath, consoleButton.Checked);
        }
    }
}