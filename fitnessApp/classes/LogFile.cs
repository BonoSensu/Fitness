using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fitnessApp
{ 
    class LogFile
    {
        public static void LogFileInput(string callingEventForLog)
        {
            // Создать файл и папку для логов
            string logDirectoryPath = @"C:\logFilePath";
            string logFilePath = @"C:\logFilePath\fitnessAppLog.txt";
            DirectoryInfo logFileDir = new DirectoryInfo(logDirectoryPath);
            if (!logFileDir.Exists)
            {
                logFileDir.Create();
            }
            //Записывать входящую строку в файл лога
            using (FileStream fsream = new FileStream(logFilePath, FileMode.Append))
            {
                string logInput = "\r\n" + Convert.ToString(DateTime.Now) + ";" + callingEventForLog;
                byte[] logInputbyte = System.Text.Encoding.Default.GetBytes(logInput);
                fsream.Write(logInputbyte, 0, logInputbyte.Length);
            }
        }
    }
}
