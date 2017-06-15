using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class changeSettings
    {
        public static int dataSet(string operation, string item)
        {
            string result = string.Empty; // строка работы для лога
            try {
                result += "данные уже содержатся, произвожу замену;";
                string changeData = string.Empty;
                if (operation == "setname")
                {
                    changeData = "UPDATE `Accounts` SET `Name`=[" + item + "] WHERE Login='" + UserData.Login + "'";
                }
                else if (operation == "setpass")
                {
                    changeData = "UPDATE `Accounts` SET `Password`=[" + item + "] WHERE Login='" + UserData.Login + "'";
                }
                else if (operation == "setTargetWeight")
                {
                    changeData = "UPDATE `Accounts` SET `TargetWeight`=[" + item + "] WHERE Login='" + UserData.Login + "'";
                }
                else if (operation == "settrainer")
                {
                    changeData = "UPDATE `Accounts` SET `TrainerName`=[" + item + "] WHERE Login='" + UserData.Login + "'";
                }
                using (MySqlCommand com2 = new MySqlCommand(changeData, connectionSettings.SQLConnection))
                {
                    try
                    {
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
                catch (Exception ex)
                {
                    result += "Ошибка поиска записи:" + Convert.ToString(ex);
                    LogFile.LogFileInput(result);
                    return 0;
                }
            }
        }
    }

