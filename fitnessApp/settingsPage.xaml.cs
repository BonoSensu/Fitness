using System;
using System.Collections;
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
    /// Логика взаимодействия для settingsPage.xaml
    /// </summary>
    public partial class settingsPage : Page
    {
        public settingsPage()
        {
            InitializeComponent();
            FindAllTrainers allTrainers = new FindAllTrainers(connectionSettings.SQLConnection);
            ArrayList trainersList = allTrainers.findThemAll();// получаем список тренеров
            foreach (string a in trainersList)
            {
                comboBox.Items.Add(a);// Добавить тренеров для выбора
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            changeSettings.dataSet("setname", textBoxName.Text);
        }

        private void textBoxName_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonPass_Click(object sender, RoutedEventArgs e)
        {
            changeSettings.dataSet("setpass", textBoxPass.Text);
        }

        private void buttonName_Copy1_Click(object sender, RoutedEventArgs e)
        {
            changeSettings.dataSet("setTargetWeight", textBoxTargetWeght.Text);
        }

        private void buttonName_Copy2_Click(object sender, RoutedEventArgs e)
        {
            changeSettings.dataSet("settrainer", comboBox.Text);
        }
    }
}
