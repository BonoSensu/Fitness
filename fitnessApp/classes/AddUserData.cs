using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    static class AddUserData
    {
        public static int dataSet(string operation, string date, string item)
        {
            string result = string.Empty; // строка работы для лога
            // проверяем что такой записи за эту дату и с этим параметром нет
            string count = @"SELECT COUNT(*) FROM "+ UserData.Login + " Where Date=@Date AND "+ operation+" IS NULL";
            using (MySqlCommand com = new MySqlCommand(count, connectionSettings.SQLConnection))
            {
                try
                {
                    com.Parameters.Add("@user", MySqlDbType.Text).Value = UserData.Login; // Найти в таблице пользоватлей
                    com.Parameters.Add("@date", MySqlDbType.Text).Value = date; // с этой датой
                    com.Parameters.Add("@operation", MySqlDbType.Text).Value = operation; // где поле
                    com.Parameters.Add("@item", MySqlDbType.Text).Value = item; // уже содержит значение
                    Int32 counter = Convert.ToInt32(com.ExecuteScalar());
                    result += " Запрос к БД на поиск уже введеных данных; Записей в БД:"+counter;
                    if (counter < 0) // если есть - помянять
                    {
                        result += "данные уже содержатся, произвожу замену;";
                        string changeData = "INSERT @user SET " + operation+"=@item WHERE Date=@Date";
                        using (MySqlCommand com2 = new MySqlCommand(changeData, connectionSettings.SQLConnection))
                        {
                            try
                            {
                                com2.Parameters.Add("@user", MySqlDbType.Text).Value = UserData.Login; // Найти в таблице пользоватлей
                                com2.Parameters.Add("@date", MySqlDbType.Text).Value = date; // с этой датой
                                com2.Parameters.Add("@operation", MySqlDbType.Text).Value = operation; // где поле
                                com2.Parameters.Add("@item", MySqlDbType.Text).Value = item; // уже содержит значение
                                com2.ExecuteScalar();
                                result += "Данные заменены;";
                                LogFile.LogFileInput(result);
                                return 1;
                            }
                            catch (Exception ex)
                            {
                                result += "Ошибка обновления данных:" + Convert.ToString(ex);
                                LogFile.LogFileInput(result);
                                return 0;
                            }
                        }
                    }
                    else // если нет, добавим данные
                    {
                        result += "Данных не содержится, вносит в БД;";
                        string addData = @"INSERT INTO "+UserData.Login+" (Date,"+operation+") VALUES (@date,@item)";
                        using (MySqlCommand com2 = new MySqlCommand(addData, connectionSettings.SQLConnection))
                        {
                            try
                            {
                                com2.Parameters.Add("@user", MySqlDbType.Text).Value = UserData.Login; // Найти в таблице пользоватлей
                                com2.Parameters.Add("@date", MySqlDbType.Text).Value = date; // с этой датой
                                com2.Parameters.Add("@operation", MySqlDbType.Text).Value = operation; // где поле
                                com2.Parameters.Add("@item", MySqlDbType.Text).Value = item; // уже содержит значение
                                com2.ExecuteScalar();
                                result += "Данные добавлены;";
                                LogFile.LogFileInput(result);
                                return 2;
                            }
                            catch (Exception ex)
                            {
                                result += "Ошибка ввода данных:" + Convert.ToString(ex);
                                LogFile.LogFileInput(result);
                                return 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result += "Ошибка поиска записи:" + Convert.ToString(ex);
                    LogFile.LogFileInput(result);
                    return 0;
                }
            }
        }
    }
}
