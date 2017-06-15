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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fitnessApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Now;
            // Открыть окно авторизации
            try
            {
                AuthorizationWindow w = new AuthorizationWindow(this);
                w.AvaImage = AvaImage;
                w.Name_textbox = labelName;
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:"+ ex);
                LogFile.LogFileInput("Ошибка создания окна авторизации: "+ ex);
            }
        }
        // Обработчик закрытия окна
        private void labelExist_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        // открыть окно отправи сообщения
        private void labelMessage_Click(object sender, System.EventArgs e)
        {
            messagePage pgmsg = new messagePage();
            if (!UserData.userOrTrainer) pgmsg.comboBoxUsers.Visibility=System.Windows.Visibility.Visible; //показать окна выбора пользователей

            foreach (string a in getListOfUsers.getUsers())
            {
                pgmsg.comboBoxUsers.Items.Add(a);// Добавить пользователей для выбора
            }
            WievPage.Content = pgmsg;
            string messagesForUser = getAndSendMessages.GetMessage();
            pgmsg.labelMessage.Content = messagesForUser;
        }
        private void labelDairy_Click(object sender, System.EventArgs e)
        {
            dairyPage pg = new dairyPage();
            WievPage.Content = pg;
        }
        private void labelTraining_Click(object sender, System.EventArgs e)
        {
            trainingPage pg = new trainingPage();
            WievPage.Content = pg;
        }
        private void labelSettings_Click(object sender, System.EventArgs e)
        {
            settingsPage pg = new settingsPage();
            WievPage.Content = pg;
        }
        private void labelWeight_Click(object sender, System.EventArgs e)
        {
            WeightPage pg = new WeightPage();
            WievPage.Content = pg;
        }
        private void labelPhoto_Click(object sender, System.EventArgs e)
        {
            photoPage pg = new photoPage();
            WievPage.Content = pg;
        }
        /*public void constructmain()
        {

        }*/
    }
}
