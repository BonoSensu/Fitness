using System;
using System.Collections;
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
    /// Логика взаимодействия для WeightPage.xaml
    /// </summary>
    public partial class WeightPage : Page
    {
        public WeightPage()
        {
            InitializeComponent();
            DateWieght.SelectedDate = DateTime.Now;
            ArrayList histiricalData =  GetUserHistoryInfo.getInfo("CurrentWeight");
            showResult(histiricalData);
        }
        private void showResult(ArrayList histiricalData)
        {
            labelResult.Content = null;
            foreach (string data in histiricalData)
            {
                labelResult.Content +=data;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ((textBox.Text).Any(char.IsLetter)) // Проверить что только числа
            {
                MessageBox.Show("Пожалуйста введите вес в кг.");
                textBox.Text = "Введите вес";
            }
            else
            {
                int res = AddUserData.dataSet("CurrentWeight", DateWieght.Text, textBox.Text); // добавить в БД значение веса
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
            ArrayList histiricalData = GetUserHistoryInfo.getInfo("CurrentWeight");
            showResult(histiricalData);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Form1_Load(object sender, RoutedEventArgs e)
        {
        }
    }
}
