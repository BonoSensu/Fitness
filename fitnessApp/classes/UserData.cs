using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitnessApp
{
    static class UserData
    {
        public static bool userOrTrainer = true; // флаг пользователь или тренер True -пользователь
        public static int ID { get; set; }
        public static int Role { get; set; }
        public static string Name { get; set; }
        public static string Login { get; set; }
        public static double currentweight { get; set; }
        public static double targetWeight { get; set; }
        public static string trainer { get; set; }
        public static string trainerLogin { get; set; }
    }
    static class TrainerData
    {
        public static bool hasUsers = false; // Признак наличия пользователей по умолчанию false
        public static int ID { get; set; }
        public static int Role { get; set; }
        public static string Name { get; set; }
        public static string Login { get; set; }
    }
}
