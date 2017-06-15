using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class connectToBD
    {
        private string result = string.Empty; // для лога
        private MySqlConnection con = new MySqlConnection(); //  экземпляр класса подключения

        public MySqlConnection InitialiseConnection() // Поднять соединение в БД и возвратить ссылку на подключение
            {
            connectionSettings.SQLConnection = con;
                    string temp = con.State.ToString();
                    if (temp == "Open")
                    {
                        result += "Подключение к БД существует;";
                        return con;
                    }
                    else
                    {
                        MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
                        mysqlCSB.Server = connectionSettings.SQLServer; // Получаем настройски соединения из статического класса
                        mysqlCSB.Database = connectionSettings.SQLDatabase;
                        mysqlCSB.UserID = connectionSettings.SQLlogin;
                        mysqlCSB.Password = connectionSettings.SQLPassword;
                        result += "Инициация подключения к БД;";
                        try
                        {
                            con.ConnectionString = mysqlCSB.ConnectionString;
                            con.Open();
                            result += "Подключение к БД произведено;";
                         }
                        catch (Exception ex)
                        {
                            result += "Ошибка при подключении к БД;" + ex.Message;
                        }
                        LogFile.LogFileInput(result);
                        return con;
                    }
                }
            }
        }
