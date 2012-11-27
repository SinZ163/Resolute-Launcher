using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace Resolute_Launcher {
    class Snapshot {

        Resolute_Launcher mainForm;
        public Snapshot(Resolute_Launcher mainForm) {
            this.mainForm = mainForm;
        }
        public void detectSnapshotVersion() {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mainForm.DownloadProgressChanged);
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(snapshot_DownloadDataCompleted);
            mainForm.statusLabel.Text = "Checking for Updates!";
            client.DownloadDataAsync(new Uri("http://mojang.com/feed"));
        }

        void snapshot_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            mainForm.statusBar.Value = 0;
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
                    mainForm.link = lines[i];
                    mainForm.link = mainForm.link.Substring(6, mainForm.link.Length - 6); // Remove first 6.
                    mainForm.link = mainForm.link.Substring(0, mainForm.link.IndexOf("\""));
                    String[] mysplit = mainForm.link.Split('/');
                    mainForm.version = mysplit[3];
                    b = true;
                }
                i++;
            }

            Download download = new Download(mainForm);
            download.downloadAndInstall();
        }    
    }
}
