using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    class connectionSettings
    {
        //поле для хранения имени фтп-сервера
        //private static string _Host = "77.222.56.43";
        private static string _Host = "5.206.73.224";
        //поле для хранения логина фтп
        //private static string _UserName = "aforcermai";
        private static string _UserName = "users";
        //поле для хранения пароля
        //private static string _Password = "d53dfpVXr";
        private static string _Password = "123456";
        // Папка для хранения дайлов проекта
        //private static string _FTPDir ="fitnessApp";
        private static string _FTPDir = "fitnessApp";
        //поле для хранения хоста SQL сервера
        //private static string _SQLServer = "VH231.spaceweb.ru";
        private static string _SQLServer = "trykote.suroot.com";
        //поле для хранения имени базы данных
        //private static string _SQLDatabase = "aforcermai";
        private static string _SQLDatabase = "xaxukp33or";
        //поле для хранения логина к БД
        //private static string _SQLlogin = "aforcermai";
        private static string _SQLlogin = "users";
        //поле для хранения пароля к БД
        //private static string _SQLPassword = "fitnessApp";
        private static string _SQLPassword = "123456";
        //поле для хранения логина пользователя
        private static string _UserLogin;
        //поле для ссылки на подключение к SQL
        private static MySqlConnection _SQLConnection;

        public static MySqlConnection SQLConnection        
        {
            get
            {
                return _SQLConnection;
            }
            set
            {
                _SQLConnection = value;
            }
        }
        //фтп-сервер
        public static string Host
        {
            get
            {
                return _Host;
            }
            set
            {
                _Host = value;
            }
        }
        //логин
        public static string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        //пароль
        public static string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        public static string FTPDir
        {
            get
            {
                return _FTPDir;
            }
            set
            {
                _FTPDir = value;
            }
        }
        //SQL-сервер
        public static string SQLServer
        {
            get
            {
                return _SQLServer;
            }
            set
            {
                _SQLServer = value;
            }
        }
        // SQL БД
        public static string SQLDatabase
        {
            get
            {
                return _SQLDatabase;
            }
            set
            {
                _SQLDatabase = value;
            }
        }
        //SQL пароль
        public static string SQLlogin
        {
            get
            {
                return _SQLlogin;
            }
            set
            {
                _SQLlogin = value;
            }
        }
        // SQL пароль
        public static string SQLPassword
        {
            get
            {
                return _SQLPassword;
            }
            set
            {
                _SQLPassword = value;
            }
        }
        // логин пользователя
        public static string UserLogin
        {
            get
            {
                return _UserLogin;
            }
            set
            {
                _UserLogin = value;
            }
        }
    }
}
