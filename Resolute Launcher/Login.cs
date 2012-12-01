using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Resolute_Launcher {
    class Login {

/**/        Resolute_Launcher mainForm;
        String username;
        String password;
        int gamemode;
        /*
         * Normal: 0
         * Snapshot: 1
         */

        Boolean mojangAccount;
        Boolean update;
        Boolean console;

        String path;
        String rootPath;

        String link;
        String version;

        public String downloadlink = "http://s3.amazonaws.com/MinecraftDownload/";

/**/        public Login(Resolute_Launcher mainForm) {
/**/            this.mainForm = mainForm;
        }

        /*
        \\ =========================================== // 
                          Login
        // =========================================== \\
        */
        public void Init(String username, String password, int gamemode, bool update, String rootPath, bool console) {
            this.username = username;
            this.password = password;
            this.gamemode = gamemode;
            this.update = update;
            this.rootPath = rootPath;
            this.console = console;

            if (username.Contains("@"))
                mojangAccount = true;

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(login_DownloadStringCompleted);
/**/            mainForm.statusLabel.Text = "Logging in";
            client.DownloadStringAsync(new Uri("https://login.minecraft.net?user=" + username + "&password=" + password + "&version=1337"));
        }

        void login_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
/**/            mainForm.statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            String result = e.Result;
            if (result.Contains(':')) {
/**/                mainForm.rememberMe.save(username, password);

                String[] output = result.Split(':');
                if (mojangAccount) {
                    username = output[2];
                }
                String sessionID = output[3];
                String version = output[0];

                switch (gamemode) {
                    case 0:
                        path = Path.Combine(rootPath, "normal/.minecraft/bin/");
                        Environment.SetEnvironmentVariable("APPDATA", rootPath + "normal/");
                        if (update) {
                            String link = downloadlink + "minecraft.jar";
/**/                            Download download = new Download(mainForm, path, downloadlink, username, sessionID, console);
                            download.downloadAndInstall(link, version);
                        }
                        else {
                            Launch launch = new Launch(path, username, sessionID, console);
                        }
                        break;
                    case 1:
                        path = Path.Combine(rootPath, "snapshot/.minecraft/bin/");
                        Environment.SetEnvironmentVariable("APPDATA", rootPath + "snapshot/");
                        if (update) {
/**/                            mainForm.statusLabel.Text = "Checking for Updates!";
                            new Thread(() => {
                                Snapshot snapshot = new Snapshot();
                                snapshot.getLink();
                                this.link = snapshot.link;
                                this.version = snapshot.version;

/**/                                Download download = new Download(mainForm, path, downloadlink, username, sessionID, console);
                                download.downloadAndInstall(link, version);
                            }).Start();
                        }
                        else {
                            Launch launch = new Launch(path, username, sessionID, console);
                        }
                        break;
                }
            }
            else if (result.Contains("Bad")) {
/**/                mainForm.statusLabel.Text = "Incorrect username or password.";
            }
            else if (result.Contains("Premium")) {  //Un-tested.
                DialogResult result2 = MessageBox.Show("You haven't paid for minecraft, would you like to use the demo?", "User not premium", MessageBoxButtons.YesNo);
                if (result2 == DialogResult.Yes) {
                     Launch launch = new Launch(path, "Player", "-", console, true);
                }
/**/                else mainForm.statusLabel.Text = "User not premium.";

            }
            else {
/**/                mainForm.statusLabel.Text = result;
            }
        }
    }
}
