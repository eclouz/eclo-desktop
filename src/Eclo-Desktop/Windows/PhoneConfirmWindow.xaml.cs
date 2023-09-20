using Dtos.Auth;
using Integrated.ServiceLayer.User.Concrete;
using Integrated.ServiceLayer.User;
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
using System.Windows.Threading;
using Eclo_Desktop.Security;
using Eclo_Desktop.Utilities;
using System.IdentityModel.Tokens.Jwt;
using Notification.Wpf;

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for PhoneConfirmPage.xaml
    /// </summary>
    public partial class PhoneConfirmWindow : Window
    {       
        DispatcherTimer _timer;
        VerifyRegisterDto verifyRegisterDto = new VerifyRegisterDto();
        string TEL_NUMBER = string.Empty;
        IUserService userService = new UserService();
        TimeSpan _time;
        
        // Konstruktor
        public  PhoneConfirmWindow()
        {
            InitializeComponent();
             _time = TimeSpan.FromSeconds(300);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString(@"mm\:ss");
                if (_time == TimeSpan.Zero)
                { 
                    _timer!.Stop();
                    lblResendCode.Visibility = Visibility.Visible;
                }
                else lblResendCode.Visibility = Visibility.Hidden;

                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);


            _timer.Start();
           
        }

        // For Close Button
        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Application.Current.Shutdown();
            Close();
        }

        // For Minimize Button
        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // For TextBox Input
        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Label_Click(object sender, RoutedEventArgs e)
        {
       
        }
     
        // For get phone number
        public void GetPhone(string phone)
        {
            verifyRegisterDto.PhoneNumber = phone;
        }

        // For Confirm Button
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // We can get an object from the loader inside the Login button
            var loader = btnConfirm.Template.FindName("loader", btnConfirm) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loader!.Visibility = Visibility.Visible;

            // button to disable
            btnConfirm.IsEnabled = false;
            if (tb1.Text.Length > 0 & tb1.Text.Length < 6 )
            {                
                verifyRegisterDto.Code = int.Parse(tb1.Text);
                var response = await userService.VerifyRegister(verifyRegisterDto);
                if (response.result==true)
                {

                    // For Notification Success
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Success!", "Success register", NotificationType.Success);

                    //For get Singleton data
                    var identity = IdentitySingleton.GetInstance();
                    // Save Singleton Token
                    identity.Token = response.token;

                    // begin:: Tokendan ID ni yechib olish
                    var tokenInfo = DecodeJwtToken.DecodeToken(response.token);

                    if (tokenInfo.success)
                    {
                        var jwtToken = tokenInfo.token as JwtSecurityToken;

                        // Get User id
                        identity.UserId = int.Parse(jwtToken.Claims.First(claim => claim.Type == "Id").Value);
                    }
                    // end:: Tokendan ID ni yechib olish

                    _timer.Stop();
                    // For Close PhoneConfirm Window
                    this.Close();

                    // For Show Main Window
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();

                   
                }
                else
                {
                    // For the Loader to stop
                    loader.Visibility = Visibility.Collapsed;

                    // button to enable
                    btnConfirm.IsEnabled = true;

                    // For Notification Warning!
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Code Wrong", NotificationType.Warning);
                }
            }
            else
            {
                // For the Loader to stop
                loader.Visibility = Visibility.Collapsed;

                // button to enable
                btnConfirm.IsEnabled = true;

                // For Notification Warning
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", "Code Wrong", NotificationType.Warning);
            }
        }
    }
}
