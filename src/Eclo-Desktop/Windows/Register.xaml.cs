﻿using Dtos.Auth;
using Eclo_Desktop.Security;
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

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for Regiter.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IUserService userService = new UserService();
        public RegisterWindow()
        {
            InitializeComponent();
            
        }        
                
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
            
        }

        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if(tbPhone.Text.Length > 0 && tbPhone.Text.Length <= 13) { count++;  }
            if (tbPassword.Password.ToString().Length >= 8 && tbPassword.Password.ToString().Length <=32 ) { count++; }
            if (tbSecondName.Text.Length > 0) { count++; }
            if (tbName.Text.Length > 1 && tbName.Text.Length<=32) { count++; }
            if(count == 4 )
            {
                RegisterDto registerDto = new RegisterDto()
                {
                    FirstName = tbName.Text.ToString(),
                    LastName= tbSecondName.Text.ToString(),
                    PhoneNumber=tbPhone.Text.ToString(),
                    Password=tbPassword.Password.ToString()
                };
                bool response = await userService.CreateUser(registerDto);                
                if (response == true)//
                {
                    MessageBox.Show("Successfully");
                    bool response2 = await userService.SendCodeRegister(tbPhone.Text.ToString());
                    if (response2 == false) { MessageBox.Show("SMS not sended"); }
                    else
                    {

                        PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
                        phoneConfirmWindow.GetPhone(tbPhone.Text.ToString());
                        this.Hide();
                        var identity = IdentitySingleton.GetInstance();
                        var res = await userService.GetUserByPhoneNumber(tbPhone.Text, identity.Token);
                        if (res != null)
                        {
                           
                            identity.UserId = res.Id;
                            MessageBox.Show((identity.UserId).ToString());
                        }

                        phoneConfirmWindow.ShowDialog();

                    }                    
                }
                else
                {
                    MessageBox.Show("Something wrong !!");
                }

                
            }
             

        }

        private async void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
