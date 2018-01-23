using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    public class AdbHelper
    {

        public static bool CheckConnect()
        {
            string res = RunCommand("devices");
            try
            {
                if (res.Length > 30)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }

        }
        public static Stream GetMobileScreens()
        {
            if (!Directory.Exists("screensTmp"))
            {
                Directory.CreateDirectory("screensTmp");
            }
            string folder = AppDomain.CurrentDomain.BaseDirectory + "screensTmp\\";
            string fileName = DateTime.Now.ToFileTime() + ".png";
            RunCommand("shell screencap -p /sdcard/" + fileName);
            RunCommand("pull /sdcard/" + fileName + " \"" + folder + fileName + "\"");
            RunCommand("shell rm /sdcard/" + fileName);

            return File.Open(folder + fileName, FileMode.Open);
        }

        public static string RunCommand(string args)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName =
                    System.IO.Path.Combine(System.Configuration.ConfigurationManager.AppSettings["AdbPath"],
                        "adb.exe");
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;   //重定向标准输入   
                process.StartInfo.RedirectStandardOutput = true;  //重定向标准输出   
                process.StartInfo.RedirectStandardError = true;   //重定向错误输出
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                var result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return result;
            }
        }
    }
}
