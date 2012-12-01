using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Resolute_Launcher {
    class Launch {

        String path;
        String username;
        String sessionID;

        Boolean consoleEnabled;

        public Launch(String path, String username, String sessionID, Boolean consoleEnabled, Boolean demo = false) {
            this.path = path;
            this.username = username;
            this.sessionID = sessionID;
            this.consoleEnabled = consoleEnabled;

            if (demo) {
                launchDemo();
            }
            else {
                launchMinecraft();
            }
        }
        void launchMinecraft() {
            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/q /c cd " + path + " & java -Djava.library.path=\"natives\" -cp jinput.jar;lwjgl.jar;lwjgl_util.jar;minecraft.jar net.minecraft.client.Minecraft " + username + " " + sessionID);
            Process proc = new System.Diagnostics.Process();

            if (consoleEnabled == false) {
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            else {
                procStartInfo.Arguments += " &pause";
            }
            proc.StartInfo = procStartInfo;
            proc.Start();
            Application.Exit();
        }

        void launchDemo() {
            ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/q /c cd " + path + " & java -Djava.library.path=\"natives\" -cp jinput.jar;lwjgl.jar;lwjgl_util.jar;minecraft.jar net.minecraft.client.Minecraft Player - -demo");
            Process proc = new System.Diagnostics.Process();

            if (consoleEnabled == false) {
                procStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            else {
                procStartInfo.Arguments += " &pause";
            }

            proc.StartInfo = procStartInfo;
            proc.Start();
            Application.Exit();
        }
    }
}
