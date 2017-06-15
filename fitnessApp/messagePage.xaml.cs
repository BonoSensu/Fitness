﻿using System;
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
    /// Логика взаимодействия для messagePage.xaml
    /// </summary>
    public partial class messagePage : Page
    {
        public messagePage()
        {
            InitializeComponent();
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            getAndSendMessages.SendMessage(textBoxSendMessage.Text, comboBoxUsers.Text);
            getAndSendMessages.GetMessage();
            textBoxSendMessage.Text = "";
        }
    }
}
