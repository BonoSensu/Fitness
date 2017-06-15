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
using Microsoft.Win32;
using System.IO;
using System.Collections;

namespace fitnessApp
{
    /// <summary>
    /// Логика взаимодействия для NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        MySqlConnection con; // ссылка на соединение
        Window previousWindow; // привязать открытое ранее окно
        bool avaAddChoisen = false; // аватар выбран
        string pathToavatar; // путь к файлу аватара
        string extensoinOfAvatarFile; // расширение загружаемого файла
        public NewUserWindow(MySqlConnection c ,Window pWindow)
        {
            InitializeComponent();
            previousWindow = pWindow;
            radioButton.IsChecked = true;// Пользователь по умолчанию
            con = c; // Взять ссылку на подключение SQL
            FindAllTrainers allTrainers = new FindAllTrainers(con);
            ArrayList trainersList = allTrainers.findThemAll();// получаем список тренеров
            foreach (string a in trainersList)
            {
                comboBox.Items.Add(a);// Добавить тренеров для выбора
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            avaAddChoisen = true;
            Stream mystream = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "PNG Photos (*.png)|*.png";
            if (dlg.ShowDialog() != null)
            {
                try
                {
                            pathToavatar = dlg.FileName;
                            if ((pathToavatar).Contains(".jpeg")) // проверить расрешение файла на jpeg
                            {
                                extensoinOfAvatarFile = ".jpeg";
                            }
                            else if ((pathToavatar).Contains(".png"))
                            {
                                extensoinOfAvatarFile = ".png";
                            }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения медиа файла для аватара: " + ex.Message);
                    LogFile.LogFileInput("Ошибка чтения медиа файла для аватара: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string resultOfCheck = checkData();// Проверка на корректность ввода логина на англ.
            if (resultOfCheck == "Done") // Если все заполнено верно
            {
                int res;
                if (Convert.ToBoolean(radioButton.IsChecked))
                {
                    res = addNewUser.makeNewUser(con, 1, textBoxName.Text, textBoxNewLogin.Text, textBoxPass.Text, textBoxPassCurrentWeight.Text, textBoxPassTargetWeight.Text,Convert.ToString(comboBox.SelectedItem));// используем перегрузку метода
                }
                else // Если тренер, то занести его
                {
                    res = addNewUser.makeNewUser(con, 0, textBoxName.Text, textBoxNewLogin.Text, textBoxPass.Text); // используем перегрузку метода
                }
                if (res == 1)
                {
                    MessageBox.Show("Поздравляем, Вы добавлены успешно добавлены");
                    previousWindow.Show();
                    this.Close();
                    
                }
                if (res == -1)
                {
                    MessageBox.Show("Указанный логин уже существует");
                }
                // Загрузить аву если она есть
                if (avaAddChoisen==true)
                {
                    // Объект для фтп обмена
                    FtpImageUpload avaUpload = new FtpImageUpload();
                    // Создать каталог пользователя
                    avaUpload.MakeDir("/fitnessApp/", textBoxNewLogin.Text);
                    LogFile.LogFileInput("Для пользователя создан ftp аккаунт");
                    // Загрузить аватар
                    avaUpload.UploadFile("/fitnessApp/" + textBoxNewLogin.Text + "/", pathToavatar, textBoxNewLogin.Text+extensoinOfAvatarFile);
                }
            }
            else
            {
                labelError.Content = resultOfCheck;
                LogFile.LogFileInput("Допущены ошибки при создании пользователя");
            }
        }

        private string checkData()
        {
            string result = "";
            if (textBoxName.Text.Length==0) result += "Введите Ваше имя\n";
            if (!checkEng(textBoxNewLogin.Text))
            {
                result += "Логин может содержать только символы на английской раскладке или цифры\n";
                textBoxNewLogin.Clear();
            }
            if (!checkEng(textBoxPass.Text))
            {
                result += "Пароль может содержать только символы на английской раскладке или цифры\n";
                textBoxPass.Clear();
            }
            if (!checkEng(textBoxPassConfirm.Text))
            {
                result += "Пароль может содержать только символы на английской раскладке или цифры\n";
                textBoxPassConfirm.Clear();
            }
            if (textBoxNewLogin.Text.Length == 0)
            {
                result += "Пожалуйста, введите логин на английской раскладке или цифры\n";
            }
            if (textBoxPass.Text != textBoxPassConfirm.Text)
            {
                result += "Пароли не совпадают\n";
            }
            if ((textBoxPass.Text.Length==0)|| (textBoxPassConfirm.Text.Length == 0))
            {
                result += "Не введен пароль к аккаунту\n";
            }
            if (result=="") result += "Done";
            return result;
        }
        // Проверка что вес в цифрах
        private void textBoxPassTargetWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelError.Content = "";
            try
            {
                int s = Convert.ToInt32(textBoxPassTargetWeight.Text);
            }
            catch (Exception ex)
            {
                labelError.Content= "Вы ввели символ! Пожалуйста, введите цифрy";
                textBoxPassTargetWeight.Clear();
            }
        }
        // Проверка что вес в цифрах
        private void textBoxPassCurrentWeight_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            labelError.Content = "";
            try
            {
                int s = Convert.ToInt32(textBoxPassCurrentWeight.Text);
            }
            catch (Exception ex)
            {
                labelError.Content = "Вы ввели символ! Пожалуйста,введите цифрy";
                textBoxPassCurrentWeight.Clear();
            }
        }
        // Проверить что введено на англ.расскладке
        private bool checkEng(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Any(wordByte => wordByte >127)) // Проверить на англ или цифры
                {
                    return false;
                }
            }
            return true;
        }
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            radioButton1.IsChecked = false; // Если пользователь, то не тренер
            // Восстановить настройки пользователя
            label3.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;
            label5.Visibility = Visibility.Visible;
            label6.Visibility = Visibility.Visible;
            label7.Visibility = Visibility.Visible;
            textBoxPassCurrentWeight.Visibility = Visibility.Visible;
            textBoxPassTargetWeight.Visibility = Visibility.Visible;
            comboBox.Visibility = Visibility.Visible;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            radioButton.IsChecked = false; // Если тренер, то не пользователь
            // Если тренер убрать настройки пользователя
            label3.Visibility = Visibility.Collapsed;
            label4.Visibility = Visibility.Collapsed;
            label5.Visibility = Visibility.Collapsed;
            label6.Visibility = Visibility.Collapsed;
            label7.Visibility = Visibility.Collapsed;
            textBoxPassCurrentWeight.Visibility = Visibility.Collapsed;
            textBoxPassTargetWeight.Visibility = Visibility.Collapsed;
            comboBox.Visibility = Visibility.Collapsed;
        }
    }
}
