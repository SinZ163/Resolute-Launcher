using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Resolute_Launcher {
    class Download {
        String path;
        String version;
        String link;
        String downloadlink;

        String username;
        String sessionID;

        Boolean console;

        Uri native;
        Uri lwjgl;
        Uri jinput;
        Uri util;

        Resolute_Launcher mainForm;
        

        public Download(Resolute_Launcher mainForm, String path, String downloadlink, String username, String sessionID, Boolean console) {
            this.mainForm = mainForm;
            this.path = path;

            this.username = username;
            this.sessionID = sessionID;
            this.console = console;
            this.downloadlink = downloadlink;

            native = new Uri(downloadlink + "windows_natives.jar");
            lwjgl  = new Uri(downloadlink + "lwjgl.jar");
            jinput = new Uri(downloadlink + "jinput.jar");
            util   = new Uri(downloadlink + "lwjgl_util.jar");
        }
        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
/**/            mainForm.statusBar.Value = e.ProgressPercentage;
        }

        public void downloadAndInstall(String link, String version) {
            this.link = link;
            this.version = version;

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Download_DownloadFileCompleted);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(Path.Combine(path,version + ".txt"))) {
                if (File.Exists(Path.Combine(path, "minecraft.jar"))) {
                    DialogResult result = MessageBox.Show("There is an update available, Do you want to update?", "Update available", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        using (StreamWriter sw = File.CreateText(Path.Combine(path, version + ".txt"))) {
                            sw.Write(link);
                            sw.Close();
                        }
/**/                        mainForm.statusLabel.Text = "Downloading minecraft.jar";
                        client.DownloadFileAsync(new Uri(link), Path.Combine(path, "minecraft.jar"));
                    }
                    else {
                        if (!File.Exists(path + "dependancy.txt")) {
                            using (StreamWriter sw = File.CreateText(Path.Combine(path, "dependancy.txt"))) {
                                sw.Write(link);
                                sw.Close();
                            }
                            downloadAndInstallDependancys();
                        }
                        else {
/**/                            Launch launch = new Launch(path, username, sessionID, console);
                        }
                    }
                }
                else {
                    using (StreamWriter sw = File.CreateText(Path.Combine(path, version + ".txt"))) {
                        sw.Write(link);
                        sw.Close();
                    }
/**/                    mainForm.statusLabel.Text = "Downloading minecraft.jar";
                    client.DownloadFileAsync(new Uri(link), Path.Combine(path, "minecraft.jar"));
                }
            }
            else {
                if (!File.Exists(path + "dependancy.txt")) {
                    using (StreamWriter sw = File.CreateText(Path.Combine(path, "dependancy.txt"))) {
                        sw.Write(link);
                        sw.Close();
                    }
                    downloadAndInstallDependancys();
                }
                else {
/**/                            Launch launch = new Launch(path, username, sessionID, console);
                }
            }
        }

        void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            if (!File.Exists(Path.Combine(path, "dependancy.txt"))) {
                using (StreamWriter sw = File.CreateText(Path.Combine(path, "dependancy.txt"))) {
                    sw.Write(link);
                    sw.Close();
                }
                downloadAndInstallDependancys();
            }
            else {
                Launch launch = new Launch(path, username, sessionID, console);
            }
        }

        /*
        \\ ================================================= //
                        Download Dependancys
        // ================================================= \\
         */

        public void downloadAndInstallDependancys() {

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Native_DownloadFileCompleted);
/**/            mainForm.statusLabel.Text = "Downloading natives";
            client.DownloadFileAsync(native, Path.Combine(path, "natives.zip"));
        }

        void Native_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("winrar.exe", "e " + Path.Combine(path, "natives.zip") +" "+ Path.Combine(path, "natives/"));
            Process proc = new System.Diagnostics.Process();
            procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.StartInfo = procStartInfo;
            proc.Start();

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(lwjgl_DownloadFileCompleted);
/**/            mainForm.statusLabel.Text = "Downloading lwjgl";
            client.DownloadFileAsync(lwjgl, Path.Combine(path, "lwjgl.jar"));

        }

        void lwjgl_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(jinput_DownloadFileCompleted);
/**/            mainForm.statusLabel.Text = "Downloading jinput";
            client.DownloadFileAsync(jinput, Path.Combine(path, "jinput.jar"));
        }

        void jinput_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Dependancy_DownloadFileCompleted);
/**/            mainForm.statusLabel.Text = "Downloading util";
            client.DownloadFileAsync(util, Path.Combine(path, "lwjgl_util.jar"));
        }

        void Dependancy_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
/**/            mainForm.statusLabel.Text = "Launching minecraft";
            Launch launch = new Launch(path, username, sessionID, console);
        }
    }
}
