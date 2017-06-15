using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class GetUserHistoryInfo // Загрузка исторических данных пользователя
    {
        public static ArrayList getInfo(string operation)
        {
            string result = string.Empty; // строка работы для лога
            //string queryUsers = @"SELECT `Date`, @param FROM @user WHERE @param IS NOT NULL";
            string queryUsers = @"SELECT * FROM `"+UserData.Login+"` WHERE `"+operation+"` IS NOT NULL";
            result += @"Выполняется запрос типа"+ "SELECT `Date`, `"+ operation + "` FROM `"+UserData.Login+ "` WHERE `" + operation + "` IS NOT NULL'";
            ArrayList resultArray = new ArrayList();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(queryUsers, connectionSettings.SQLConnection))
                {
                    result += "Выбрать все данные в которых есть записи по тематике" + operation + ";";
                    //cmd.Parameters.Add("@param", MySqlDbType.String).Value = operation;
                    cmd.Parameters.Add("@user", MySqlDbType.String).Value = UserData.Login;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows) // Если записи есть
                        {
                            result += "Данные найдены, считываем;";
                            while (dr.Read())
                            {
                                string forInsert = string.Empty;
                                if (operation == "CurrentWeight")
                                {
                                    forInsert = "По состоянию на "+(Convert.ToString(dr.GetValue(1))).Substring(0,10) + ", ваш показатель веса составлял: " + Convert.ToString(dr.GetValue(2))+"кг.\n";
                                }
                                else if (operation == "TrainingResult")
                                {
                                    forInsert = "По состоянию на " + (Convert.ToString(dr.GetValue(1))).Substring(0, 10) + ", На тренировке вы израсходовали: " + Convert.ToString(dr.GetValue(3)) + "ккал.\n";
                                }
                                else if (operation == "ccal")
                                {
                                    forInsert = "По состоянию на " + (Convert.ToString(dr.GetValue(1))).Substring(0, 10) + ", Вы употребили: " + Convert.ToString(dr.GetValue(4)) + "ккал.\n";
                                }
                                result += "Найдены поля данных;";
                                resultArray.Add(forInsert);
                            }
                            LogFile.LogFileInput(result);
                            return resultArray;
                        }
                        else
                        {
                            string forInsert = "Записи по полю" + operation + " У пользователя отсутствуют";
                            resultArray.Add(forInsert);
                            result = "Записи по полю" + operation + " У пользователя отсутствуют;";
                            LogFile.LogFileInput(result);
                            return resultArray;
                        }
                    }
                }
            }
            catch (Exception ex) { 
                result += "Ошибка обновления данных:" + Convert.ToString(ex);
                LogFile.LogFileInput(result);
                return resultArray;
            }
        }
    }
}
