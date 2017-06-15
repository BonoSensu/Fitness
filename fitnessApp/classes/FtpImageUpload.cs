using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace fitnessApp
{
    class FtpImageUpload
    {
        //объект для запроса данных
        FtpWebRequest ftpRequest;

        //объект для получения данных
        FtpWebResponse ftpResponse;

        //метод протокола FTP STOR для загрузки файла на FTP-сервер
        public void UploadFile(string path, string fileName,string login)
        {
            try
            {
            
            FileStream uploadedFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            LogFile.LogFileInput(path);
            ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + connectionSettings.Host + path + login);
            ftpRequest.Credentials = new NetworkCredential(connectionSettings.UserName, connectionSettings.Password);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            //Буфер для загружаемых данных
            byte[] file_to_bytes = new byte[uploadedFile.Length];
            //Считываем данные в буфер
            uploadedFile.Read(file_to_bytes, 0, file_to_bytes.Length);

            uploadedFile.Close();

            //Поток для загрузки файла 
            Stream writer = ftpRequest.GetRequestStream();

            writer.Write(file_to_bytes, 0, file_to_bytes.Length);
            writer.Close();
                LogFile.LogFileInput("Аватар загружен");
            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Ошибка загрузки аватара"+ Convert.ToString(ex));
            }
        }
        //Метод для создания каталога для пользователя
        public void MakeDir(string path, string folderName)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + connectionSettings.Host + path + folderName);

                ftpRequest.Credentials = new NetworkCredential(connectionSettings.UserName, connectionSettings.Password);
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                LogFile.LogFileInput("На Ftp создана папка для пользователя");
            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Ошибка создания папки для пользователя"+ Convert.ToString(ex));
            }
        }
    }
    }

