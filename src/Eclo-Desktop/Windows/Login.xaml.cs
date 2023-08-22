﻿using Dtos.Auth;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
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
using System.Xml.Linq;

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IUserService userService = new UserService();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regiterWindow = new RegisterWindow();
            regiterWindow.ShowDialog();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (tbPhone.Text.Length > 0 && tbPhone.Text.Length <= 13) { count++; }
            if (tbPassword.Password.ToString().Length >= 8 && tbPassword.Password.ToString().Length <= 32) { count++; }            
            if (count == 2)
            {
                LoginDto loginDto = new LoginDto()
                {
                    PhoneNumber = tbPhone.Text,
                    Password = tbPassword.Password
                };
                bool response = await userService.Login(loginDto);
                if (response)
                {
                    MessageBox.Show("You are logged in ");
                }
                else
                {
                    MessageBox.Show("Something wrong !!");
                }
            }
        }
    }
}
