using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class GetInfoAboutUser
    {
        public ArrayList trainerUsers = new ArrayList(); // коллекция пользователей

        public bool getInfo(MySqlConnection con, string login)
        {
            string query = @"Select ID,Role,Name,Login,currentWeight,TargetWeight,TrainerName FROM Accounts WHERE Login = @log";
            using (MySqlCommand com = new MySqlCommand(query, con))
            {
                try
                {
                    com.Parameters.Add("@log", MySqlDbType.Text).Value = login;
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int role = Convert.ToInt32(dr.GetValue(1));
                            if (role == 1) // Если пользователь
                            {
                                UserData.ID = Convert.ToInt32(dr.GetValue(0));
                                UserData.Role = role;
                                UserData.Name = Convert.ToString(dr.GetValue(2));
                                UserData.Login =connectionSettings.UserLogin = login;
                                UserData.currentweight = Convert.ToDouble(dr.GetValue(4));
                                UserData.targetWeight = Convert.ToDouble(dr.GetValue(5));
                                UserData.trainer = Convert.ToString(dr.GetValue(6));
                                LogFile.LogFileInput("Данные пользователя загружены в класс UserData");
                                dr.Close();
                                // загрузить логин тренера
                                    string query2 = @"Select Login FROM Accounts WHERE Name = @name";
                                    using (MySqlCommand com2 = new MySqlCommand(query2, con))
                                    {
                                        com.Parameters.Add("@name", MySqlDbType.Text).Value = UserData.trainer;
                                        using (MySqlDataReader dr2 = com.ExecuteReader())
                                        {
                                            while (dr.Read())
                                            {
                                                UserData.trainerLogin = Convert.ToString(dr.GetValue(0));
                                            }
                                        }
                                    }
                                return true;
                            }
                            else if (role == 0) // если трененр
                            {
                                UserData.userOrTrainer = false;// если тренер то сделать флаг в false
                                TrainerData.ID = Convert.ToInt32(dr.GetValue(0));
                                TrainerData.Role = role;
                                TrainerData.Name = Convert.ToString(dr.GetValue(2));
                                TrainerData.Login= connectionSettings.UserLogin = login;
                                LogFile.LogFileInput("Данные тренера загружены в класс TrainerData"+Convert.ToString(TrainerData.Role));
                                dr.Close();
                                getTrainerUser(con); // Если тренер, то собрать его пользователей через метод
                                return false;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogFile.LogFileInput("Ошибка сбора данных о пользователя" + Convert.ToString(ex));
                    return false;
                }
                return false;
            }
        }
            public void getTrainerUser(MySqlConnection con)
        {
            // Соберем логины и имена всех пользователей
            string queryUsers = @"Select Name,Login FROM Accounts WHERE TrainerName = @trainerName";
            using (MySqlCommand comUsers = new MySqlCommand(queryUsers, con))
            {
                try
                {
                    comUsers.Parameters.Add("@trainerName", MySqlDbType.Text).Value = TrainerData.Name;
                    using (MySqlDataReader dr2 = comUsers.ExecuteReader())
                    {
                        LogFile.LogFileInput("Сбор информации о пользователях тренера"+Convert.ToString(dr2.HasRows));
                        if (dr2.HasRows) // если пользователей не найдено
                        {
                            TrainerData.hasUsers = true;
                            LogFile.LogFileInput("У тренера есть пользователи");
                        }
                        while (dr2.Read())
                        {
                            LogFile.LogFileInput("Список пользователей:" + Convert.ToString(dr2.GetValue(1)));
                            trainerUsers.Add(Convert.ToString(dr2.GetValue(1)));
                            trainerUsers.Add(Convert.ToString(dr2.GetValue(0)));
                        }
                        dr2.Close();
                    }
                }
                catch (Exception ex)
                {
                    LogFile.LogFileInput("Ошибка сбора данных о пользователях тренера" + Convert.ToString(ex));
                }
            }
        }
        
    }
}
