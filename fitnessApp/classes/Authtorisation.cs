using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class Authtorisation
    {
        private string login = string.Empty;
        private string password = string.Empty;
        private string result = string.Empty; // для лога
        //Получае данные о запрашиваемом логине и пароле в конструкторе класса
        public Authtorisation(string log, string pass)
        {
            login = log;
            password = pass;
        }
        public int CheckStatus(MySqlConnection con, string loginStr, string passwordStr)
        {
            string query = @"Select ID,Role,Name,Login,Password,currentWeight,TargetWeight,TrainerName FROM Accounts WHERE Login = @log AND Password = @pass";
            using (MySqlCommand com = new MySqlCommand(query, con))
            {
                try
                {
                    com.Parameters.Add("@log", MySqlDbType.Text).Value = loginStr;
                    com.Parameters.Add("@pass", MySqlDbType.Text).Value = passwordStr;
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        result += " Запрос к БД выполнен успешно";
                        if (dr.HasRows)
                        {
                            result += "Пользователь найден;";
                            result += " Загруженны данные пользователя: " + loginStr + ";";
                            LogFile.LogFileInput(result);
                            return 1;
                        }
                        else
                        {
                            result += "Некорректная попытка авторизации;";
                            LogFile.LogFileInput(result);
                            return 0; // Пользователь  не найден
                        }
                    }
                }
                catch (Exception ex)
                {
                    result += " Ошибка работы с БД:" + ex.Message;
                    LogFile.LogFileInput(result);
                    return -1; // ошибка работы с БД
                }
            }
        }
    }
}
