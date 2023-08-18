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
    public partial class RegiterWindow : Window
    {
        public RegiterWindow()
        {
            InitializeComponent();
        }        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if(tbPhone.Text.Length > 0 && tbPhone.Text.Length <= 13) { count++;  }
            if (tbPassword.Text.Length >= 8 && tbPassword.Text.Length <=32 ) { count++; }
            if (tbSecondName.Text.Length > 0) { count++; }
            if (tbName.Text.Length > 1 && tbName.Text.Length<=32) { count++; }
            if(count == 4 )
            {
                PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
                phoneConfirmWindow.ShowDialog();
            }
        }

        private void btnToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
    }
}
