using Dtos.Auth;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Eclo_Desktop.Utilities;

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
            Application.Current.Shutdown();
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegisterWindow regiterWindow = new RegisterWindow();
            regiterWindow.ShowDialog();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            
            //PhoneNumber Validation
            if (tbPhone.Text.Length > 0 && tbPhone.Text.Length <= 13) { count++; }
            
            //Password Validation
            if (tbPassword.Password.ToString().Length >= 8 && tbPassword.Password.ToString().Length <= 32) { count++; }            
            
            if (count == 2)
            {
                //Login Dto
                LoginDto loginDto = new LoginDto()
                {
                    PhoneNumber = tbPhone.Text,
                    Password = tbPassword.Password
                };

                var response = await userService.Login(loginDto);
                if (response.result)
                {
                    var identity = IdentitySingleton.GetInstance();
                    identity.Token = response.token;

                    //begin:: Tokendan ID ni yechib olish
                    var tokenInfo = DecodeJwtToken.DecodeToken(response.token);

                    if (tokenInfo.success)
                    {
                        var jwtToken = tokenInfo.token as JwtSecurityToken;

                        // Foydalanuvchi ID'sini olish
                        identity.UserId  =int.Parse( jwtToken.Claims.First(claim => claim.Type == "Id").Value);
                        
                    }            
                    //end:: Tokendan ID ni yechib olish
                    
                    
                    this.Hide();
                    MainWindow mainWindow = new MainWindow();   
                    mainWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Something wrong !!");
                }
            }
        }
    }
}
