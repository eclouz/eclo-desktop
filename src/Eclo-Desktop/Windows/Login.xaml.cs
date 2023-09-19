using Dtos.Auth;
using Eclo_Desktop.Security;
using Eclo_Desktop.Utilities;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using Notification.Wpf;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Create an object from UserService
        IUserService userService = new UserService();

        //For get Singleton data
        IdentitySingleton identity = IdentitySingleton.GetInstance();
        
        // Konstruktor
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Button Close
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        // Button Minimize
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Button Cancel
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        // Button Registration
        private  void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // We can get an object from the loader inside the Registration button
            var loader = btnToLogin.Template.FindName("loader", btnToLogin) as FontAwesome.WPF.ImageAwesome;
            
            // For the Loader to run
            loader!.Visibility = Visibility.Visible;

            //button to disable
            btnToLogin.IsEnabled = false;

            // For Login Window Hide
            this.Hide();
            
            // For Open Register Window
            RegisterWindow regiterWindow = new RegisterWindow();
            regiterWindow.ShowDialog();

            // For the Loader to stop
            loader.Visibility = Visibility.Collapsed;
            
            // button to enable
            btnSave.IsEnabled = true;
        }

        // Button Login
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //For textbox
            Regex rx_phone = new Regex(@"^[\+][0-9]{3}?[0-9]{9}$");
            Regex rx_password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");
            
            // We can get an object from the loader inside the Login button
            var loader = btnSave.Template.FindName("loader", btnSave) as FontAwesome.WPF.ImageAwesome;
            
            // For the Loader to run
            loader!.Visibility = Visibility.Visible;

            // button to disable
            btnSave.IsEnabled = false;


            int count = 0;

            // PhoneNumber Validation
            if (rx_phone.IsMatch(tbPhone.Text)) { count+=1; }

            // Password Validation
            if (rx_password.IsMatch(tbPassword.Password.ToString())) { count+=2; }

            if (count == 3)
            {
                // Login Dto
                LoginDto loginDto = new LoginDto()
                {
                    PhoneNumber = tbPhone.Text,
                    Password = tbPassword.Password
                };

                // For login Send request 
                var response = await userService.Login(loginDto);

                if (response.result == true)
                {
                    //Save Singleton Token
                    identity.Token = response.token;

                    // begin:: Tokendan ID ni yechib olish
                    var tokenInfo = DecodeJwtToken.DecodeToken(response.token);

                    if (tokenInfo.success)
                    {
                        var jwtToken = tokenInfo.token as JwtSecurityToken;

                        // Get User id
                        identity.UserId = int.Parse(jwtToken.Claims.First(claim => claim.Type == "Id").Value);
                    }
                    //end:: Tokendan ID ni yechib olish

                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnSave.IsEnabled = true;

                    // For Login Window Hide
                    this.Hide();

                    //For Main Window Show
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                }
                else
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnSave.IsEnabled = true;

                    // For Phone Number Error Notification
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "User not found", NotificationType.Warning, "WindowArea");

                }
            }
            else if (count == 2)
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Phone number wrong", NotificationType.Warning, "WindowArea");
            }
            else if (count == 1)
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Password wrong", NotificationType.Warning,"WindowArea");
            }

            else
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Phone number and password wrong", NotificationType.Warning,"WindowArea");


            }
        }
    }
}
