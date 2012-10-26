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
using hide;
using System.Reflection;


namespace Resolute_Launcher {
    public partial class Resolute_Launcher : Form {

        public String username;
        public String password;
        public String result;
        public String sessionID;
        public String version;
        public Boolean mojangAccount;

        //public int completed = 1;

        public String link = "";
        String downloadlink = "http://s3.amazonaws.com/MinecraftDownload/";

        public String path;

        String rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\.resolute\\";


        /*
         \\ =========================================== // 
                             Login
         // =========================================== \\
         */
        private void launchButton_Click(object sender, EventArgs e) {
            username = userText.Text;
            password = passText.Text;

            if (username.Contains("@"))
                mojangAccount = true;

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(login_DownloadStringCompleted);
            statusLabel.Text = "Logging in";
            client.DownloadStringAsync(new Uri("https://login.minecraft.net?user=" + username + "&password=" + password + "&version=1337"));
        }

        void login_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            result = e.Result;

            if (result.Contains(':')) {
                if (rememberBox.Checked) {
                    String hash;
                    Crypto crypto = new Crypto();
                    crypto.init();
                    hash = crypto.encrypt(password, username);

                    String settings;
                    if (updateButton.Checked) {
                        settings = "1";
                    }
                    else {
                        settings = "0";
                    }

                    if (consoleButton.Checked) {
                        settings += "1";
                    }
                    else {
                        settings += "0";
                    }

                    if (snapshotButton.Checked) {
                        settings += "1";
                    }
                    else {
                        settings += "0";
                    }

                    if (File.Exists(rootPath + "rememberMe.txt")) {
                        File.Delete(rootPath + "rememberMe.txt");
                        using (StreamWriter sw = File.CreateText(rootPath + "rememberMe.txt")) {
                            sw.WriteLine(username);
                            sw.WriteLine(hash);
                            sw.WriteLine(settings);
                            sw.Close();
                        }
                    }
                    else {
                        using (StreamWriter sw = File.CreateText(rootPath + "rememberMe.txt")) {
                            sw.WriteLine(username);
                            sw.WriteLine(hash);
                            sw.WriteLine(settings);
                            sw.Close();
                        }
                    }
                }


                String[] output = result.Split(':');
                if (mojangAccount) {
                    username = output[2];
                }
                sessionID = output[3];
                version = output[0];

                if (snapshotButton.Checked) {
                    path = rootPath + "snapshot\\.minecraft\\bin\\";
                    Environment.SetEnvironmentVariable("APPDATA", rootPath + "snapshot\\");
                    if (updateButton.Checked == true) {
                        detectSnapshotVersion();
                    }
                    else {
                        launchMinecraft();
                    }

                }
                else if (normalButton.Checked) {
                    path = rootPath + "normal\\.minecraft\\bin\\";
                    Environment.SetEnvironmentVariable("APPDATA", rootPath + "normal\\");
                    if (updateButton.Checked == true) {
                        link = downloadlink + "minecraft.jar";
                        downloadAndInstall();
                    }
                    else {
                        launchMinecraft();
                    }
                }
            }
            else {
                statusLabel.Text = result;
            }
        }

        /*
        \\ ==================================================== //
                        Check for Snapshot Update
        // ==================================================== \\
        */
        public void detectSnapshotVersion() {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(snapshot_DownloadDataCompleted);
            statusLabel.Text = "Checking for Updates!";
            client.DownloadDataAsync(new Uri("http://mojang.com/feed"));
        }

        void snapshot_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            byte[] webPage = { };
            webPage = e.Result;

            String lineArray = Encoding.UTF8.GetString(webPage);
            String[] lines = lineArray.Split(new Char[] { });
            int i = 0;
            bool b = false;
            while (b != true) {
                if (lines[i].Contains("assets") & lines[i].Contains("minecraft.jar")) {
                    link = lines[i];
                    link = link.Substring(6, link.Length - 6); // Remove first 6.
                    link = link.Substring(0, link.IndexOf("\""));
                    String[] mysplit = link.Split('/');
                    version = mysplit[3];
                    b = true;
                }
                i++;
            }

            downloadAndInstall();
        }

        /*
         \\ ================================================== //
                         Download Minecraft.jar
         // ================================================== \\
         */

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
                        statusLabel.Text = "Downloading minecraft.jar";
                        client.DownloadFileAsync(new Uri(link), path + "minecraft.jar");
                    } else {
                        if (!File.Exists(path + "dependancy.txt")) {
                            using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                                sw.Write(link);
                                sw.Close();
                            }
                            downloadAndInstallDependancys();
                        }
                        else {
                            launchMinecraft();
                        }
                    }
                }
                else {
                    using (StreamWriter sw = File.CreateText(path + version + ".txt")) {
                        sw.Write(link);
                        sw.Close();
                    }
                    statusLabel.Text = "Downloading minecraft.jar";
                    client.DownloadFileAsync(new Uri(link), path + "minecraft.jar");
                }
            } else {
                if (!File.Exists(path + "dependancy.txt")) {
                    using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                        sw.Write(link);
                        sw.Close();
                    }
                    downloadAndInstallDependancys();
                } else {
                    launchMinecraft();
                }
            }
        }

        void Download_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            if (!File.Exists(path + "dependancy.txt")) {
                using (StreamWriter sw = File.CreateText(path + "dependancy.txt")) {
                    sw.Write(link);
                    sw.Close();
                }
                downloadAndInstallDependancys();
            } else {
                launchMinecraft();
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

            statusLabel.Text = "Downloading natives";
            client.DownloadFileAsync(native, path + "natives.zip");
        }

        void Native_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            statusBar.Value = 0;
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
            statusLabel.Text = "Downloading lwjgl";
            client.DownloadFileAsync(lwjgl, path + "lwjgl.jar");

        }

        void lwjgl_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(jinput_DownloadFileCompleted);
            Uri jinput = new Uri(downloadlink + "jinput.jar");
            statusLabel.Text = "Downloading jinput";
            client.DownloadFileAsync(jinput, path + "jinput.jar");
        }

        void jinput_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Dependancy_DownloadFileCompleted);
            Uri util = new Uri(downloadlink + "lwjgl_util.jar");
            statusLabel.Text = "Downloading util";
            client.DownloadFileAsync(util, path + "lwjgl_util.jar");
        }

        void Dependancy_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            statusBar.Value = 0;
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            statusLabel.Text = "Launching minecraft";
            launchMinecraft();
        }

        /*
        \\ ==================================================== //
                            Launch Minecraft.jar
        // ==================================================== \\
        */

        public void launchMinecraft() {
            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/q /c cd %appdata%\\.minecraft\\bin & java -Djava.library.path=\"natives\" -cp jinput.jar;lwjgl.jar;lwjgl_util.jar;minecraft.jar net.minecraft.client.Minecraft " + username + " " + sessionID);
            Process proc = new System.Diagnostics.Process();

            if (consoleButton.Checked == false) {
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
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
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            if (File.Exists(rootPath + "rememberMe.txt")) {
                String hash;
                String settings;
                using (StreamReader sr = File.OpenText(rootPath + "rememberMe.txt")) {
                    userText.Text = sr.ReadLine();
                    hash = sr.ReadLine();
                    settings = sr.ReadLine();
                    sr.Close();
                }
                Crypto crypto = new Crypto();
                crypto.init();
                passText.Text = crypto.decrypt(hash, userText.Text);

                Char[] settingArray = settings.ToCharArray();
                byte i = 0;
                while (i < 3) {
                    switch (i) {
                        case 0:
                            if (settingArray[0] == '1') {
                                updateButton.Checked = true;
                            }
                            else {
                                updateButton.Checked = false;
                            }
                            break;
                        case 1:
                            if (settingArray[1] == '1') {
                                consoleButton.Checked = true;
                            }
                            else {
                                consoleButton.Checked = false;
                            }
                            break;
                        case 2:
                            if (settingArray[2] == '1') {
                                snapshotButton.Checked = true;
                            }
                            else {
                                normalButton.Checked = true;
                            }
                            break;
                    }
                    i++;
                }
 
                rememberBox.Checked = true;
            }
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
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            statusBar.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e) {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }
    }
}