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
    /// Логика взаимодействия для trainingPage.xaml
    /// </summary>
    public partial class trainingPage : Page
    {
        public trainingPage()
        {
            InitializeComponent();
            DateTrening.SelectedDate = DateTime.Now;
            ArrayList histiricalData = GetUserHistoryInfo.getInfo("TrainingResult");
            showResult(histiricalData);
        }
        private void showResult(ArrayList histiricalData)
        {
            labelresult.Content = null;
            foreach (string data in histiricalData)
            {
                labelresult.Content += data;
            }
        }

        private void buttonTraining_Click(object sender, RoutedEventArgs e)
        {
            if ((textTraining.Text).Any(char.IsLetter)) // Проверить что только числа
            {
                MessageBox.Show("Пож-та введите затраченные ккал");
                textTraining.Text = "";
            }
            else
            {
                int res = AddUserData.dataSet("TrainingResult", DateTrening.Text, textTraining.Text); // добавить в БД значение веса
                if (res == 0)
                {
                    MessageBox.Show("Ошибка ввода данных");
                }
                else if (res == 1)
                {
                    MessageBox.Show("Данные за выбраную дату обновлены");
                }
                else if (res == 2)
                {
                    MessageBox.Show("Данные за выбраную дату добавлены");
                }
            }
            ArrayList histiricalData = GetUserHistoryInfo.getInfo("TrainingResult");
            showResult(histiricalData);
        }
    }
}
