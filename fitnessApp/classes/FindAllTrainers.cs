using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;

namespace fitnessApp
{
    class FindAllTrainers
    {
        private MySqlConnection con;
        public FindAllTrainers(MySqlConnection c) // Конструктор класса, передается ссылка на подключение
        {
            con = c;
        }
        public ArrayList findThemAll()
        {
            string result = string.Empty; // Сборка строки для лога
            string query = @"Select Name FROM Accounts WHERE Role = 0"; // Найти всех с ролью тренер
            ArrayList arrayOfTrainers = new ArrayList(); // создаем необобщенную коллекцию
            using (MySqlCommand com = new MySqlCommand(query, con))
            {
                try
                {
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        result += " Запрос к БД на поиск тренера выполнен успешно";
                        if (dr.HasRows)
                        { 
                            result += "Тренер найден;";
                        }
                        else
                        {
                            result += "Тренеры не надены;";
                            LogFile.LogFileInput(result);
                            arrayOfTrainers.Add("Not finded");// Тренер  не найден
                        }
                        while (dr.Read())
                        {
                            arrayOfTrainers.Add(dr.GetValue(0)); // Загружаем тренеров в необощенную коллекцию List
                        }
                        dr.Close();
                        LogFile.LogFileInput(result);
                    }
                }
                catch (Exception ex)
                {
                    result += " Ошибка работы с БД:" + ex.Message;
                    LogFile.LogFileInput(result);
                }
                return arrayOfTrainers;
            }
        }
    }
}
