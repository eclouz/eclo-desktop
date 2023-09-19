using Dtos.Auth;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        // Create an object from UserService
        IUserService userService = new UserService();

        // Konstruktor
        public RegisterWindow()
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
        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Button Cancel
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            this.Close();
        }

        // Button Register
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // We can get an object from the loader inside the Login button
            var loader = btnSave.Template.FindName("loader", btnSave) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loader!.Visibility = Visibility.Visible;

            // button to disable
            btnSave.IsEnabled = false;

            // For textboxs validations
            Regex rx_phone = new Regex(@"^[\+][0-9]{3}?[0-9]{9}$");
            Regex rx_password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");
            Regex rx_name = new Regex(@"^[a-z,A-Z]{3,25}$");

            // For count validation
            string count = "";

            // For check TextBox 
            if(rx_phone.IsMatch(tbPhone.Text)) { count+="t";  }
            if (rx_password.IsMatch(tbPassword.Password.ToString())) { count += "p"; }
            if (rx_name.IsMatch(tbSecondName.Text)) { count += "f"; }
            if (rx_name.IsMatch(tbName.Text)) { count += "n"; }

            if (count == "tpfn")
            {
                // For Register Dto
                RegisterDto registerDto = new RegisterDto()
                {
                    FirstName = tbName.Text.ToString(),
                    LastName = tbSecondName.Text.ToString(),
                    PhoneNumber = tbPhone.Text.ToString(),
                    Password = tbPassword.Password.ToString()
                };

                // For register Send request 
                bool response = await userService.CreateUser(registerDto);


                if (response == true)
                {
                    // For Send Code Register
                    bool res_send_code = await userService.SendCodeRegister(tbPhone.Text.ToString());

                    if (res_send_code == false)
                    {
                        // For the Loader to stop
                        loader.Visibility = Visibility.Collapsed;

                        // button to enable
                        btnSave.IsEnabled = true;

                        //For Notification Send Code Warning
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Error!", "Something Error not send code", NotificationType.Error);
                    }
                    else
                    {
                        //For Notification Send Code Successful
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Successful!", "Send code your phone number", NotificationType.Success);
                        
                        // For PhoneConfirWindow give phone_number
                        PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
                        phoneConfirmWindow.GetPhone(tbPhone.Text.ToString());

                        // For the Loader to stop
                        loader.Visibility = Visibility.Collapsed;

                        // button to enable
                        btnSave.IsEnabled = true;

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
                    btnSave.IsEnabled = true;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Error!", "Something Error", NotificationType.Error);
                }
            }
            else if (!count.Contains("n"))
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Name wrong", NotificationType.Warning);
            }
            else if (!count.Contains("f"))
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Surname wrong", NotificationType.Warning);
            }
           
            else if (!count.Contains("p"))
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Password wrong", NotificationType.Warning);
            }
            else if (!count.Contains("t"))
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Phone number wrong", NotificationType.Warning);
            }   
            else  
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnSave.IsEnabled = true;

                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "You have filled in the lines incorrectly", NotificationType.Warning);
            }
        }

        // Button Show Login
        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            // For Close Register Window
            this.Close();

            // Fow Show Login Window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
