using Eclo_Desktop.Pages;
using Eclo_Desktop.Themes;
using Eclo_Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eclo_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //RegiterWindow regiterWindow = new RegiterWindow();
            //this.Hide();
            //regiterWindow.ShowDialog();

            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.ShowDialog();

            //PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
            //phoneConfirmWindow.ShowDialog();            

            Dashboard dashboard = new Dashboard();
            PageNavigator.Content = dashboard;
            //this.ShowDialog();
        }

        private void rbDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            PageNavigator.Content = dashboard;
            //PageNavigator.Navigate(new Uri("Men.xaml", UriKind.Relative));
        }

        private void rbProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsPage page = new ProductsPage();
            PageNavigator.Content = page;
        }

        private void rbProfile_Click(object sender, RoutedEventArgs e)
        {
            UserPage userPage = new UserPage();
            PageNavigator.Content = userPage;
        }

        private void rbUser_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage page = new SettingsPage();
            PageNavigator.Content = page;
        }

        private void rbAboutUs_Click(object sender, RoutedEventArgs e)
        {
            AboutUsPage aboutUsPage = new AboutUsPage();
            PageNavigator.Content = aboutUsPage;
        }

        private void rbFaq_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rbSaveLists_Click(object sender, RoutedEventArgs e)
        {
            SaveListPage saveListPage   = new SaveListPage();
            PageNavigator.Content = saveListPage;
        }

        private void chkbox_Click(object sender, RoutedEventArgs e)
        {
            //if (chkbox.IsChecked == true)
            //{
            //    AppTheme.ChangeTheme(new Uri("/Themes/DarkTheme.xaml", UriKind.Relative));

            //}
            //else
            //{
            //    AppTheme.ChangeTheme(new Uri("/Themes/LightTheme.xaml", UriKind.Relative));
            //}
        }
    }
}
