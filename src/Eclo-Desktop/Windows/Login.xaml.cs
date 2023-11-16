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
using System.Xml.Linq;

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

        //============================BUTTONS LOGIN and REGISTER=================================================== 

        // Button Login
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //For textbox
            Regex rx_phone = new Regex(@"^[\+][0-9]{3}?[0-9]{9}$");
            Regex rx_password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");

            // We can get an object from the loader inside the Login button
            var loader = btnLogin.Template.FindName("loader", btnLogin) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loader!.Visibility = Visibility.Visible;

            // button to disable
            btnLogin.IsEnabled = false;


            int count = 0;

            // PhoneNumber Validation
            if (rx_phone.IsMatch(tbLoginPhone.Text)) { count += 1; }

            // Password Validation
            if (rx_password.IsMatch(tbLoginPassword.Password.ToString())) { count += 2; }
            

            if (count == 3)
            {
                try
                {
                    // Login Dto
                    LoginDto loginDto = new LoginDto()
                    {
                        PhoneNumber = tbLoginPhone.Text,
                        Password = tbLoginPassword.Password
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
                        btnLogin.IsEnabled = true;

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
                        btnLogin.IsEnabled = true;

                        // For Phone Number Error Notification
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Warning!", "User not found", NotificationType.Warning, "WindowArea");

                    }
                }
                catch
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnLogin.IsEnabled = true;

                    // For Phone Number Error Notification
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Connection Error", NotificationType.Warning, "WindowArea");
                }
            }
            else if (count == 2)
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnLogin.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Phone number wrong", NotificationType.Warning, "WindowArea");
            }
            else if (count == 1)
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnLogin.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Password wrong", NotificationType.Warning, "WindowArea");
            }

            else
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnLogin.IsEnabled = true;

                // For Phone Number Error Notification
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Phone number and password wrong", NotificationType.Warning, "WindowArea");


            }
        }

        //Button Register
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // We can get an object from the loader inside the Login button
                var loader = btnRegister.Template.FindName("loader", btnRegister) as FontAwesome.WPF.ImageAwesome;

                // For the Loader to run
                loader!.Visibility = Visibility.Visible;

                // button to disable
                btnRegister.IsEnabled = false;

                // For textboxs validations
                Regex rx_phone = new Regex(@"^[\+][0-9]{3}?[0-9]{9}$");
                Regex rx_password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");
                Regex rx_name = new Regex(@"^[a-z_',A-Z_']{3,25}$");

                // For count validation
                string count = "";

                // For check TextBox 
                if (rx_phone.IsMatch(tbRegisterPhone.Text)) { count += "t"; }
                if (rx_password.IsMatch(tbRegisterPassword.Password.ToString())) { count += "p"; }
                if (rx_name.IsMatch(tbRegisterSecondName.Text)) { count += "f"; }
                if (rx_name.IsMatch(tbRegisterName.Text)) { count += "n"; }

                if (count == "tpfn")
                {
                    // For Register Dto
                    RegisterDto registerDto = new RegisterDto()
                    {
                        FirstName = tbRegisterName.Text.ToString(),
                        LastName = tbRegisterSecondName.Text.ToString(),
                        PhoneNumber = tbRegisterPhone.Text.ToString(),
                        Password = tbRegisterPassword.Password.ToString()
                    };

                    // For register Send request 
                    var response = await userService.CreateUser(registerDto);


                    if (response.result == true)
                    {
                        // For Send Code Register
                        bool res_send_code = await userService.SendCodeRegister(tbRegisterPhone.Text.ToString());

                        if (res_send_code == false)
                        {
                            // For the Loader to stop
                            loader.Visibility = Visibility.Collapsed;

                            // button to enable
                            btnRegister.IsEnabled = true;

                            //For Notification Send Code Warning
                            var notificationManager = new NotificationManager();
                            notificationManager.Show("Error!", "Something Error not send code", NotificationType.Error, RowsCountWhenTrim: 2);
                        }
                        else
                        {
                            //For Notification Send Code Successful
                            var notificationManager = new NotificationManager();
                            notificationManager.Show("Successful!", "Send code your phone number", NotificationType.Success, RowsCountWhenTrim: 2);

                            // For PhoneConfirWindow give phone_number
                            PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
                            phoneConfirmWindow.GetPhone(tbRegisterPhone.Text.ToString());

                            // For the Loader to stop
                            loader.Visibility = Visibility.Collapsed;

                            // button to enable
                            btnRegister.IsEnabled = true;

                            // For Hide RegisterWindow
                            this.Hide();
                            // For Show PhoneConfirm Window
                            phoneConfirmWindow.ShowDialog();

                        }
                    }
                    else
                    {
                        // For the Loader to stop
                        loader.Visibility = Visibility.Collapsed;

                        // button to enable
                        btnRegister.IsEnabled = true;

                        var notificationManager = new NotificationManager();
                        notificationManager.Show(response.result_text, NotificationType.Warning, RowsCountWhenTrim: 2);
                    }
                }
                else if (!count.Contains("n"))
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnRegister.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Name wrong", NotificationType.Warning, RowsCountWhenTrim: 2);
                }
                else if (!count.Contains("f"))
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnRegister.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Surname wrong", NotificationType.Warning, RowsCountWhenTrim: 2);
                }

                else if (!count.Contains("p"))
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnRegister.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Password wrong", NotificationType.Warning, RowsCountWhenTrim: 2);
                }
                else if (!count.Contains("t"))
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnRegister.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Phone number wrong", NotificationType.Warning, RowsCountWhenTrim: 2);
                }
                else
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnRegister.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "You have filled in the lines incorrectly", NotificationType.Warning, RowsCountWhenTrim: 2);
                }
            }
            catch
            {

                // button to enable
                btnRegister.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Connection Error", NotificationType.Warning, RowsCountWhenTrim: 2);
            }

        }


        //=============================CANCEL BUTTON Login and Regiter Pages========================================

        // Login Cancel Button
        private void btnLoginCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        //Register Cancel Button
        private void btnRegisterCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

       
        //===============================Change Register Or Login BUTTONS=============================================
        
        // Login ToRegister Button
        private void btnToRegister_Click(object sender, RoutedEventArgs e)
        {
          
            LoginLeftBorder.Visibility = Visibility.Collapsed;
            LoginRightBorder.Visibility = Visibility.Collapsed;

            RegisterLeftBorder.Visibility=Visibility.Visible;
            RegisterRightBorder.Visibility = Visibility.Visible;
            
        }

        // Register ToLogin Button
        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterLeftBorder.Visibility = Visibility.Collapsed;
            RegisterRightBorder.Visibility = Visibility.Collapsed;

            LoginLeftBorder.Visibility = Visibility.Visible;
            LoginRightBorder.Visibility = Visibility.Visible;
        }
    }
}
