using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace fitnessApp
{ 
    static class addNewUser
    {
        public static int makeNewUser(MySqlConnection con, int role, string Name, string Login, string Pass, string CurrentWeight, string TargetWeight, string trainerName) 
        {
            string result = string.Empty; // Сборка строки для лога
            // Проверим существование талицы, есои нет - создаем
            try
            {
                LogFile.LogFileInput("Попытка проверки наличия таблицы Account");
                //string makeNewTable = @"CREATE TABLE `aforcermai`.`Accounts` (  `ID` INT NOT NULL AUTO_INCREMENT , `Role` INT NOT NULL , `Name` TEXT NOT NULL , `Login` TEXT NOT NULL , `Password` TEXT NOT NULL , `currentWeight` DOUBLE NOT NULL , `TargetWeight` DOUBLE NOT NULL , `TrainerName` TEXT NOT NULL ) ENGINE = InnoDB";
                string makeNewTable = @"CREATE TABLE `xaxukp33or`.`Accounts` (  `ID` INT(1) NOT NULL AUTO_INCREMENT PRIMARY KEY, `Role` INT NOT NULL , `Name` TEXT NOT NULL , `Login` TEXT NOT NULL , `Password` TEXT NOT NULL , `currentWeight` DOUBLE NOT NULL , `TargetWeight` DOUBLE NOT NULL , `TrainerName` TEXT NOT NULL ) ENGINE = InnoDB";
                using (MySqlCommand cmdTable = new MySqlCommand(makeNewTable, con))
                {
                    cmdTable.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Таблица Accounts уже существует");
            }
            // Проверим что такого логина еще нет
            string count = @"SELECT COUNT(*) FROM Accounts Where Login=@Login";
            using (MySqlCommand com = new MySqlCommand(count, con))
            {
                try
                {
                    com.Parameters.Add("@Login", MySqlDbType.Text).Value = Login;
                    Int32 counter = Convert.ToInt32(com.ExecuteScalar());
                    result += " Запрос к БД на поиск аналогичных логинов;";
                    if (counter != 0)
                    {
                        result += "Ошибка, такой login уже есть;";
                            LogFile.LogFileInput(result);
                            return -1; //возвращаем ошибку, такой логин есть
                        }
                        else // если нет, добавим пользователя
                        {
                        result += "Попытка добавления данных пользователя;";
                        double cweight = double.Parse(CurrentWeight); // конвертируем  строку в число
                        double tweight = double.Parse(TargetWeight); // конвертируем  строку в число
                        string query = @"INSERT INTO Accounts (Role,Name,Login,Password,currentWeight,TargetWeight,TrainerName) VALUES(@role,@Name,@Login,@Pass,@CurrentWeight,@TargetWeight,@trainerName)";
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            
                            cmd.Parameters.Add("@role", MySqlDbType.Int32).Value = role;
                            cmd.Parameters.Add("@Name", MySqlDbType.Text).Value = Name;
                            cmd.Parameters.Add("@Login", MySqlDbType.Text).Value = Login;
                            cmd.Parameters.Add("@Pass", MySqlDbType.Text).Value = Pass;
                            cmd.Parameters.Add("@CurrentWeight", MySqlDbType.Text).Value = cweight;
                            cmd.Parameters.Add("@TargetWeight", MySqlDbType.Text).Value = TargetWeight;
                            cmd.Parameters.Add("@trainerName", MySqlDbType.Text).Value = tweight;
                            cmd.ExecuteNonQuery();
                            result += "Данные добавлены в Accounts";
                            }
                        }
                    // Создать таблицу для результатов каждого пользователя
                    //string makeNewTable = @"CREATE TABLE `aforcermai`.`" + Login + "` ( `ID` INT NOT NULL AUTO_INCREMENT , `Date` TEXT NULL DEFAULT NULL , `CurrentWeight` DOUBLE NULL DEFAULT NULL, `TrainingResult` DOUBLE NULL DEFAULT NULL, `ccal` TEXT NULL DEFAULT NULL, `trainingCcal` TEXT NULL DEFAULT NULL,  PRIMARY KEY (`ID`)) ENGINE = InnoDB;)";
                    string makeNewTable = @"CREATE TABLE `xaxukp33or`.`" + Login + "` ( `ID` INT(1) NOT NULL AUTO_INCREMENT PRIMARY KEY , `Date` TEXT NULL DEFAULT NULL , `CurrentWeight` DOUBLE NULL DEFAULT NULL, `TrainingResult` DOUBLE NULL DEFAULT NULL, `ccal` TEXT NULL DEFAULT NULL, `trainingCcal` TEXT NULL DEFAULT NULL)  ENGINE = InnoDB";
                    LogFile.LogFileInput("Добавление таблицы пользваотеля в БД");
                    using (MySqlCommand cmd = new MySqlCommand(makeNewTable, con))
                    {
                        cmd.ExecuteNonQuery();
                        LogFile.LogFileInput(result);
                        return 1;
                    }
                    }
                catch (Exception ex)
                {
                    result += " Ошибка работы с БД:" + ex.Message;
                    LogFile.LogFileInput(result);
                    return 1; // Ошибка работы с БД
                }
            }
        }
        public static int makeNewUser(MySqlConnection con, int role, string Name, string Login, string Pass)
        {
            string result = string.Empty; // Сборка строки для лога
            // Проверим существование талицы, есои нет - создаем
            try
            {
                LogFile.LogFileInput("Попытка проверки наличия таблицы Account");
                //string makeNewTable = @"CREATE TABLE `aforcermai`.`Accounts` (  `ID` INT NOT NULL AUTO_INCREMENT , `Role` INT NOT NULL , `Name` TEXT NOT NULL , `Login` TEXT NOT NULL , `Password` TEXT NOT NULL , `currentWeight` DOUBLE NOT NULL , `TargetWeight` DOUBLE NOT NULL , `TrainerName` TEXT NOT NULL ) ENGINE = InnoDB";
                string makeNewTable = @"CREATE TABLE `xaxukp33or`.`Accounts` (  `ID` INT(1) NOT NULL AUTO_INCREMENT PRIMARY KEY, `Role` INT NOT NULL , `Name` TEXT NOT NULL , `Login` TEXT NOT NULL , `Password` TEXT NOT NULL , `currentWeight` DOUBLE , `TargetWeight` DOUBLE , `TrainerName` TEXT ) ENGINE = InnoDB";
                using (MySqlCommand cmdTable = new MySqlCommand(makeNewTable, con))
                {
                    cmdTable.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogFile.LogFileInput("Таблица Accounts уже существует");
            }
            // Проверим что такого логина еще нет
            string count = @"Select Count(*) FROM Accounts Where Login=@login";
            using (MySqlCommand com = new MySqlCommand(count, con))
            {
                try
                {
                    com.Parameters.Add("@login", MySqlDbType.Text).Value = Login;
                    result += "Инициализация подсчета логинов;";
                    Int32 counter = Convert.ToInt32(com.ExecuteScalar());
                        result += " Запрос к БД на поиск аналогичных логинов;";
                        if (counter != 0)
                        {
                            result += "Ошибка, такой login уже есть;" + counter;
                            LogFile.LogFileInput(result);
                            return -1; //возвращаем ошибку, такой логин есть
                        }
                        else // если нет, добавим пользователя
                        {
                        result += " Попытка добавления данных тренера;";
                        string query = @"INSERT INTO Accounts (Role,Name,Login,Password) VALUES (@role,@Name,@Login,@Pass)";
                            using (MySqlCommand cmd = new MySqlCommand(query, con))
                            {
                            cmd.Parameters.Add("@role", MySqlDbType.Int32).Value = role;
                            cmd.Parameters.Add("@Name", MySqlDbType.Text).Value = Name;
                            cmd.Parameters.Add("@Login", MySqlDbType.Text).Value = Login;
                            cmd.Parameters.Add("@Pass", MySqlDbType.Text).Value = Pass;
                            cmd.ExecuteNonQuery();
                                result += " Данные добавлены;";
                            LogFile.LogFileInput(result);
                        }
                        // Создать таблицу для тренера и его сообщений пользователям
                        //string makeNewTable = @"CREATE TABLE `xaxukp33or`.`" + Login+"` ( `ID` INT(1) NOT NULL AUTO_INCREMENT PRIMARY KEY , `ToUserLogin` TEXT NOT NULL , `Message` TEXT NOT NULL) ENGINE = InnoDB";
                        string makeNewTable = @"CREATE TABLE " + Login + "` ( `ID` INT(1) NOT NULL AUTO_INCREMENT PRIMARY KEY , `ToUserLogin` TEXT NOT NULL , `Message` TEXT NOT NULL) ENGINE = InnoDB";
                        using (MySqlCommand cmdTable = new MySqlCommand(makeNewTable, con))
                        {
                            cmdTable.ExecuteNonQuery();
                        }
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    result += " Ошибка работы с БД:" + ex.Message;
                    LogFile.LogFileInput(result);
                    return 1; // Ошибка работы с БД
                }
            }
        }
    }
}
