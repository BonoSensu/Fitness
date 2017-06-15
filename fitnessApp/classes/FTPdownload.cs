using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace fitnessApp
{
    class FTPdownload
    {
        public static void DownloadMediaFiles()
        {
            try
            {
                string Images = @"C:\logFilePath\" + connectionSettings.UserName;
                DirectoryInfo ImagesDir = new DirectoryInfo(Images);
                if (!ImagesDir.Exists)
                {
                    ImagesDir.Create();
                }
                string[] files = GetFileList();
                foreach (string file in files)
                {
                    Download(file);
                }
            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Ошибка загрузки файлов, медиа файлы пользователя отсутствуют");
            }
        }

        private static string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create("ftp://" + connectionSettings.Host +"/"+ connectionSettings.FTPDir+"/"+ connectionSettings.UserLogin);
                LogFile.LogFileInput("ftp://" + connectionSettings.Host + "/" + connectionSettings.FTPDir + "/" + connectionSettings.UserLogin);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(connectionSettings.UserName, connectionSettings.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                    LogFile.LogFileInput(line);
                }
                // \
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Ошибка загрузки медиафайлов:");
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        private static void Download(string file)
        {
            try
            {
                string uri = "ftp://" + connectionSettings.Host + "/" + connectionSettings.FTPDir + "/" + connectionSettings.UserLogin + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp) // Проверка что строка запроса ftp корректна
                {
                    return;
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                LogFile.LogFileInput("Чтение файла настроек медиа:" + uri);
                reqFTP.Credentials = new NetworkCredential(connectionSettings.UserName, connectionSettings.Password);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                LogFile.LogFileInput("Подключение успешно произведено");
                Stream responseStream = response.GetResponseStream();
                LogFile.LogFileInput("Запись медиафайла:" + @"C:\logFilePath\" + connectionSettings.UserLogin + "\\" + file);
                FileStream writeStream = new FileStream(@"C:\logFilePath\"+connectionSettings.UserLogin + "\\" + file, FileMode.Create);                
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
