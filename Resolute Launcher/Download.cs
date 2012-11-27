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
        
        Resolute_Launcher mainForm;

        public Download(Resolute_Launcher mainForm) {
            this.mainForm = mainForm;
            this.path = mainForm.path;
            this.version = mainForm.version;
            this.link = mainForm.link;
            this.downloadlink = mainForm.downloadlink;
        }
        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            mainForm.statusBar.Value = e.ProgressPercentage;
        }

        public void downloadAndInstall() {

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Download_DownloadFileCompleted);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path + version + ".txt")) {
                if (File.Exists(path + "minecraft.jar")) {
                    DialogResult result = MessageBox.Show("There is an update available, Do you want to update?", "Update available", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        using (StreamWriter sw = File.CreateText(path + version + ".txt")) {
                            sw.Write(link);
                            sw.Close();
                        }
                        mainForm.statusLabel.Text = "Downloading minecraft.jar";
                        client.DownloadFileAsync(new Uri(link), path + "minecraft.jar");
                    }
                    else {
                        if (!File.Exists(path + "dependancy.txt")) {
                            using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                                sw.Write(link);
                                sw.Close();
                            }
                            downloadAndInstallDependancys();
                        }
                        else {
                            mainForm.launchMinecraft();
                        }
                    }
                }
                else {
                    using (StreamWriter sw = File.CreateText(path + version + ".txt")) {
                        sw.Write(link);
                        sw.Close();
                    }
                    mainForm.statusLabel.Text = "Downloading minecraft.jar";
                    client.DownloadFileAsync(new Uri(link), path + "minecraft.jar");
                }
            }
            else {
                if (!File.Exists(path + "dependancy.txt")) {
                    using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                        sw.Write(link);
                        sw.Close();
                    }
                    downloadAndInstallDependancys();
                }
                else {
                    mainForm.launchMinecraft();
                }
            }
        }

        void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            if (!File.Exists(path + "dependancy.txt")) {
                using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                    sw.Write(link);
                    sw.Close();
                }
                downloadAndInstallDependancys();
            }
            else {
                mainForm.launchMinecraft();
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

            Uri native = new Uri(downloadlink + "windows_natives.jar");

            mainForm.statusLabel.Text = "Downloading natives";
            client.DownloadFileAsync(native, path + "natives.zip");
        }

        void Native_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("winrar.exe", "e " + path + "natives.zip " + path + "natives\\");
            Process proc = new System.Diagnostics.Process();
            procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.StartInfo = procStartInfo;
            proc.Start();

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(lwjgl_DownloadFileCompleted);
            Uri lwjgl = new Uri(downloadlink + "lwjgl.jar");
            mainForm.statusLabel.Text = "Downloading lwjgl";
            client.DownloadFileAsync(lwjgl, path + "lwjgl.jar");

        }

        void lwjgl_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(jinput_DownloadFileCompleted);
            Uri jinput = new Uri(downloadlink + "jinput.jar");
            mainForm.statusLabel.Text = "Downloading jinput";
            client.DownloadFileAsync(jinput, path + "jinput.jar");
        }

        void jinput_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Dependancy_DownloadFileCompleted);
            Uri util = new Uri(downloadlink + "lwjgl_util.jar");
            mainForm.statusLabel.Text = "Downloading util";
            client.DownloadFileAsync(util, path + "lwjgl_util.jar");
        }

        void Dependancy_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            mainForm.statusLabel.Text = "Launching minecraft";
            mainForm.launchMinecraft();
        }
    }
}
