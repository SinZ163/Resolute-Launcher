using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace Resolute_Launcher {
    class Snapshot {

        public String link;
        public String version;

        public void getLink() {
            WebClient client = new WebClient();

            byte[] webPage = { };
            try {
                webPage = client.DownloadData(new Uri("http://mojang.com/feed"));
            }
            catch (Exception e) {
                MessageBox.Show(e.StackTrace);
            }
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
        }   
    }
}
