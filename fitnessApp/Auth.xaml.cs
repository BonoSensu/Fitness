using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace fitnessApp
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        bool loginInserted = false;
        bool passInserted = false;
        MySqlConnection con;// экземляр объекта для ссылки на подключение
        Window mainWindow;
        public Image AvaImage { get; set; } // ссылка на загрузку анатара
        public Label Name_textbox { get; set; } // Сслыка на загрузку имени
        public AuthorizationWindow(Window w)
        {
            InitializeComponent();
            connectToBD conBD = new connectToBD(); //Создаем объект подключения к БД;
            con = conBD.InitialiseConnection(); // Получить ссылку на подключение
            mainWindow = w; // ссылка на главный экран
            this.Closing += (obj, arg) => mainWindow.Close(); // Делегат для закрытия. Не авторизовался - не показывать главный экран
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!loginInserted||!passInserted)
            {
                MessageBox.Show("Пожалуйста введите логин и пароль");
            }
            else
            {
                Authtorisation auth = new Authtorisation(textBoxLogin.Text, passwordBox.Password);// создаем экземпляр для авторизации
                int authtorisationResult = auth.CheckStatus(con, textBoxLogin.Text, passwordBox.Password); // передать сылку на подключение к БД логин и пароль
                if (authtorisationResult==1)
                {
                    //Проверяем есть ли такой пользователь
                    LogFile.LogFileInput("Пройдена авторизация с параметрами: Логин:" + textBoxLogin.Text + "");
                    /*foreach (var item in connectionSettings.UserData)
                    {
                        MessageBox.Show(Convert.ToString(item));
                    }
                    foreach (var item in connectionSettings.TrainerData)
                    {
                        MessageBox.Show(Convert.ToString(item));
                    }*/
                    GetInfoAboutUser userInfo = new fitnessApp.GetInfoAboutUser();// объект для получения данных по пользователю
                    userInfo.getInfo(con, textBoxLogin.Text); // выполняем запрос получения данных по логину
                    /*if (!UserData.userOrTrainer)
                    {
                        MessageBox.Show("Выбран тренер у него есть пользователи:"+Convert.ToString(TrainerData.hasUsers));
                        try
                        {
                            foreach (object user in userInfo.trainerUsers)
                            {
                                    MessageBox.Show("Ваши пользователи:" + Convert.ToString(user));
                                }
                        }
                        catch (Exception ex)
                        {
                            LogFile.LogFileInput("Ошибка загрузки пользователей тренера" + Convert.ToString(ex));
                        }
                    }*/
                    FTPdownload.DownloadMediaFiles(); // загрузить локальную папку все медиа этого пользователя
                    // Загрузить аватар
                    try
                    {
                        BitmapImage bm1 = new BitmapImage();
                        if (UserData.userOrTrainer) //Если пользователь
                        {
                            Name_textbox.Content = UserData.Name;
                            bm1.BeginInit();
                            bm1.UriSource = new Uri(@"C:\logFilePath\" + UserData.Login + "\\" + UserData.Login + ".png", UriKind.Relative);
                            LogFile.LogFileInput("Установка аватара:" + @"C:\logFilePath\" + UserData.Login + "\\" + UserData.Login + ".png");
                            bm1.CacheOption = BitmapCacheOption.OnLoad;
                            bm1.EndInit();
                            AvaImage.Stretch = Stretch.Fill;
                            AvaImage.Source = bm1;
                        }
                        else // если тренер
                        {
                            Name_textbox.Content = TrainerData.Name;
                            bm1.BeginInit();
                            bm1.UriSource = new Uri(@"C:\logFilePath\" + TrainerData.Login + "\\" + TrainerData.Login + ".png", UriKind.Relative);
                            LogFile.LogFileInput("Установка аватара:" + @"C:\logFilePath\" + TrainerData.Login + "\\" + TrainerData.Login + ".png");
                            bm1.CacheOption = BitmapCacheOption.OnLoad;
                            bm1.EndInit();
                            AvaImage.Stretch = Stretch.Fill;
                            AvaImage.Source = bm1;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogFile.LogFileInput("Ошибка загрузки аватара - пользователь не прикрепил файлы");
                    }
                    mainWindow.Show();
                    this.Hide();
                }
                else if (authtorisationResult==0)
                {
                    labelInfo.Content = "Такого пользователя не существует";
                    LogFile.LogFileInput("Некорректная попытка авторизации: Логин:" + textBoxLogin.Text + "");
                }
                else if (authtorisationResult == -1)
                {
                    LogFile.LogFileInput("Ошибка работы с БД");
                }
            }
        }
        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            loginInserted = true;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passInserted = true;
        }

        private void buttonNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(con, this);// Открыть окно регистрации нового пользователя
            newUserWindow.Show();
            mainWindow.Hide();
            this.Hide();
        }

        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_newSettings_Click(object sender, RoutedEventArgs e)
        {
            // изменить настройки подключения к 

            if (textBox_SQLServer.Text != "")
            {
                connectionSettings.SQLServer = textBox_SQLServer.Text;
            }
            if (textBox__SQLDatabase.Text != "")
            {
                connectionSettings.SQLServer = textBox__SQLDatabase.Text;
            }
            if (textBox_Login.Text != "")
            {
                connectionSettings.SQLlogin = textBox_Login.Text;
            }
            if (textBox_Password.Text != "")
            {
                connectionSettings.SQLPassword = textBox_Password.Text;
            }
            if (textBox_Host.Text != "")
            {
                connectionSettings.Host = textBox_Host.Text;
            }
            if (textBox_FTPLogin.Text != "")
            {
                connectionSettings.UserName = textBox_FTPLogin.Text;
            }
            if (textBox_FTPPassword.Text != "")
            {
                connectionSettings.Password = textBox_FTPPassword.Text;
            }
        }

        private void textBox_FTPPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
