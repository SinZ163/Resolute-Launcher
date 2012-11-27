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
        public String password;
        public String result;
        public String sessionID;
        public String version;
        public Boolean mojangAccount;

        public rememberMe rememberMe;

        //public int completed = 1;

        public String link = "";
        public String downloadlink = "http://s3.amazonaws.com/MinecraftDownload/";

        public String path;

        public String rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\.resolute\\";





        /*
        \\ ==================================================== //
                            Launch Minecraft.jar
        // ==================================================== \\
        */

        public void launchMinecraft() {
            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/q /c cd "+path+" & java -Djava.library.path=\"natives\" -cp jinput.jar;lwjgl.jar;lwjgl_util.jar;minecraft.jar net.minecraft.client.Minecraft " + username + " " + sessionID);
            Process proc = new System.Diagnostics.Process();

            if (consoleButton.Checked == false) {
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            else {
                procStartInfo.Arguments += " &pause";
            }

            proc.StartInfo = procStartInfo;
            proc.Start();
            Application.Exit();
        }
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
                Directory.Delete(rootPath + "snapshot\\.minecraft\\bin\\", true);
            }
            else if (normalButton.Checked == true) {
                Directory.Delete(rootPath + "normal\\.minecraft\\bin\\", true);
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

        public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            statusBar.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e) {
            aboutResolute about = new aboutResolute();
            about.Show();
        }
    }
}