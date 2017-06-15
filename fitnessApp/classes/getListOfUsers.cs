using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    static class getListOfUsers
    {
        public static ArrayList getUsers()
        {
            ArrayList resultArray = new ArrayList();
            string result = string.Empty;
            string query = @"Select Login FROM Accounts WHERE TrainerName = @name";
            using (MySqlCommand com = new MySqlCommand(query, connectionSettings.SQLConnection))
            {
                try
                {
                    com.Parameters.Add("@name", MySqlDbType.Text).Value = TrainerData.Login;
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        result += " Запрос к БД на поиск пользователей тренера выполнен успешно";
                        while (dr.Read())
                        {
                            resultArray.Add(Convert.ToString(dr.GetValue(0)));
                        }
                        LogFile.LogFileInput(result);
                        return resultArray;
                    }
                }
                catch (Exception ex)
                {
                    result += " Ошибка работы с БД при загрузке пользователей тренера:" + ex.Message;
                    LogFile.LogFileInput(result);
                    resultArray.Add("У вас нет пользователей");
                    return resultArray;
                }
            }
        }
    }
}
