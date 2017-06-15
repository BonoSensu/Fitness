using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    static class getAndSendMessages
    {
        public static string GetMessage() // Получение сообщения
        {
            string result = string.Empty;
            string allMessages = string.Empty;
            try
            {
                MySqlConnection com = connectionSettings.SQLConnection;
                if (UserData.userOrTrainer)// если пользователь
                {
                    string query = @"Select Message FROM @trainerLogin WHERE ToUserLogin = @userlogin";
                    using (MySqlCommand cmd = new MySqlCommand(query, com))
                    {
                        cmd.Parameters.Add("@trainerLogin", MySqlDbType.Text).Value = UserData.trainerLogin;
                        cmd.Parameters.Add("@userlogin", MySqlDbType.Text).Value =UserData.Login;
                        using (MySqlDataReader dr2 = cmd.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                allMessages += Convert.ToString(dr2.GetValue(0))+"\n"; 
                            }
                            LogFile.LogFileInput("Сообщения для пользователю от тренера доставлены");
                            return allMessages;
                        }
                    }
                }
                else
                {
                    string query = @"Select * FROM '" + UserData.trainerLogin+"'";
                    using (MySqlCommand cmd = new MySqlCommand(query, com))
                    {
                        using (MySqlDataReader dr2 = cmd.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                allMessages += "Сообщение от:" +Convert.ToString(dr2.GetValue(1)) +" текст" +Convert.ToString(dr2.GetValue(2)) + "\n";
                            }
                            LogFile.LogFileInput("Сообщения для тренера загружены");
                            return allMessages;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result += "Ошибка получения сообщения"+Convert.ToString(ex);
                LogFile.LogFileInput(result);
                return "Ошибка чтения сообщений см.лог файл";
            }
        }

        public static void SendMessage(string msg, string destUser)// ОТправка сообщения
        {
            string result = string.Empty;
            try {
                MySqlConnection com = connectionSettings.SQLConnection;
                if (UserData.userOrTrainer)// если пользователь
                {
                    string query = @"INSERT INTO " + UserData.trainerLogin + " (ToUserLogin,Message) VALUES(@ToUserLogin,@Message)";
                    using (MySqlCommand cmd = new MySqlCommand(query, com))
                    {
                        cmd.Parameters.Add("@ToUserLogin", MySqlDbType.Text).Value = UserData.Login;
                        cmd.Parameters.Add("@Name", MySqlDbType.Text).Value = "=>"+msg;
                        cmd.ExecuteNonQuery();
                        result += "Сообщение отправлено";
                    }
                }
                else { // Если тренер
                    string query = @"INSERT INTO " + UserData.trainerLogin + " (ToUserLogin,Message) VALUES(@ToUserLogin,@Message)";
                    using (MySqlCommand cmd = new MySqlCommand(query, com))
                    {
                        cmd.Parameters.Add("@ToUserLogin", MySqlDbType.Text).Value = destUser;
                        cmd.Parameters.Add("@Name", MySqlDbType.Text).Value = "<=" + msg;
                        cmd.ExecuteNonQuery();
                        result += "Сообщение отправлено";
                    }
                }
                LogFile.LogFileInput(result);
            }
            catch (Exception ex)
            {
                result += "Ошибка отправки сообщения";
                LogFile.LogFileInput(result);
            }
    }
    }
}
