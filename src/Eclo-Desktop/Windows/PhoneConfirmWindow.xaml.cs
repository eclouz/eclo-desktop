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
        public  PhoneConfirmWindow()
        {
            InitializeComponent();
             _time = TimeSpan.FromSeconds(300);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString(@"mm\:ss");
                if (_time == TimeSpan.Zero)
                { 
                    _timer.Stop();
                    lblResendCode.Visibility = Visibility.Visible;
                }
                else lblResendCode.Visibility = Visibility.Hidden;

                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);


            _timer.Start();
           
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

        private void tb1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Label_Click(object sender, RoutedEventArgs e)
        {
       
        }
     
        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GetPhone(string phone)
        {
            verifyRegisterDto.PhoneNumber = phone;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {            
            
            if(tb1.Text.Length > 0 & tb1.Text.Length < 6 )
            {
                
                verifyRegisterDto.Code = int.Parse(tb1.Text);
                bool response = await userService.VerifyRegister(verifyRegisterDto);
                if (response==true)
                {
                    MessageBox.Show("You are Veerified !!");
                    var res = await userService.GetUserByPhoneNumber(verifyRegisterDto.PhoneNumber);
                    if (res != null)
                    {
                        var identity = IdentitySingleton.GetInstance();
                        identity.UserId = res.Id;
                    }
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                    Close();
                }
            }
        }
    }
}
