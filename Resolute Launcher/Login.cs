using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace Resolute_Launcher {
    class Login {

        Resolute_Launcher mainForm;

        public Login(Resolute_Launcher mainForm) {
            this.mainForm = mainForm;
        }

        /*
        \\ =========================================== // 
                          Login
        // =========================================== \\
        */
        public void launchButton_Click(object sender, EventArgs e) {
            mainForm.username = mainForm.userText.Text;
            mainForm.password = mainForm.passText.Text;

            if (mainForm.username.Contains("@"))
                mainForm.mojangAccount = true;

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mainForm.DownloadProgressChanged);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(login_DownloadStringCompleted);
            mainForm.statusLabel.Text = "Logging in";
            client.DownloadStringAsync(new Uri("https://login.minecraft.net?user=" + mainForm.username + "&password=" + mainForm.password + "&version=1337"));
        }

        void login_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            mainForm.result = e.Result;

            if (mainForm.result.Contains(':')) {
                mainForm.rememberMe.save();

                String[] output = mainForm.result.Split(':');
                if (mainForm.mojangAccount) {
                    mainForm.username = output[2];
                }
                mainForm.sessionID = output[3];
                mainForm.version = output[0];

                if (mainForm.snapshotButton.Checked) {
                    mainForm.path = mainForm.rootPath + "snapshot/.minecraft/bin/";
                    Environment.SetEnvironmentVariable("APPDATA", mainForm.rootPath + "snapshot/");
                    if (mainForm.updateButton.Checked == true) {
                        Snapshot snapshot = new Snapshot(mainForm);
                        snapshot.detectSnapshotVersion();
                    }
                    else {
                        mainForm.launchMinecraft();
                    }

                }
                else if (mainForm.normalButton.Checked) {
                    mainForm.path = mainForm.rootPath + "normal/.minecraft/bin/";
                    Environment.SetEnvironmentVariable("APPDATA", mainForm.rootPath + "normal/");
                    if (mainForm.updateButton.Checked == true) {
                        mainForm.link = mainForm.downloadlink + "minecraft.jar";
                        Download download = new Download(mainForm);
                        download.downloadAndInstall();
                    }
                    else {
                        mainForm.launchMinecraft();
                    }
                }
            }
            else if (mainForm.result.Contains("Bad")) {
                mainForm.statusLabel.Text = "Incorrect username or password.";
            }
            else {
                mainForm.statusLabel.Text = mainForm.result;
            }
        }
    }
}
