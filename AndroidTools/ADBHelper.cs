using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTools
{
    class ADBHelper
    {
        static readonly string ADB_FILE_NAME = "tools/adb.exe";

        private static string ExecuteCmd(string cmd)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = ADB_FILE_NAME;
            p.StartInfo.Arguments = cmd;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();

            p.StandardInput.AutoFlush = true;

            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();
            p.Close();

            return output;
        }

        public static List<string> GetDeviceList()
        {
            string resultStr = ExecuteCmd("devices");

            List<string> deviceList = new List<string>(resultStr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

            if (deviceList.Count < 2 || deviceList[deviceList.Count - 1].Contains("* daemon started successfully *"))
                return null;

            if (deviceList[1].Contains("daemon not running"))
                deviceList.RemoveRange(0, 3);
            else
                deviceList.RemoveAt(0);

            for (int i = 0; i < deviceList.Count; ++i)
            {
                deviceList[i] = deviceList[i].Split('\t')[0];
            }

            return deviceList;
        }

        public static string GetDeviceModel(string deviceName)
        {
            return ExecuteCmd("-s " + deviceName + " shell getprop ro.product.model");
        }
    }
}
