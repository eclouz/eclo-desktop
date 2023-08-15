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

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for PhoneConfirmPage.xaml
    /// </summary>
    public partial class PhoneConfirmWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public  PhoneConfirmWindow()
        {
            InitializeComponent();

             _time = TimeSpan.FromSeconds(15);



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
    }
}
