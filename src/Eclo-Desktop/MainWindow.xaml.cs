using Eclo_Desktop.Pages;
using Eclo_Desktop.Security;
using Eclo_Desktop.Themes;
using Eclo_Desktop.Windows;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
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
using static Eclo_Desktop.Components.Dashboards.ProductLightClothesUserControl;

namespace Eclo_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void RefreshPageHandlerDelegate();

    public partial class MainWindow : Window
    {
        private readonly IUserService _userService;
        private readonly RefreshPageHandlerDelegate refreshDelegate;

        public MainWindow()
        {
            InitializeComponent();
            this._userService = new UserService();
            refreshDelegate = new RefreshPageHandlerDelegate(RefreshPageHandler);
        }
        public async  Task refreshAsync()
        {
            var identity = IdentitySingleton.GetInstance();
            var result = await _userService.GetUserById(identity.UserId);

            lblUserName.Content = result.FirstName;
            lblCountry.Content = result.Region;

            string imageUrl = "https://localhost:7190/" + result.ImagePath;
            Uri uri = new Uri(imageUrl, UriKind.Absolute);
            brUserImage.ImageSource = new BitmapImage(uri);

            Dashboard dashboard = new Dashboard();
            PageNavigator.Content = dashboard;
        }
        public async void RefreshPageHandler()
        {
            await refreshAsync();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await refreshAsync();

            //RegisterWindow regiterWindow = new RegisterWindow();
            //this.Hide();
            //regiterWindow.ShowDialog();

            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.ShowDialog();

            //PhoneConfirmWindow phoneConfirmWindow = new PhoneConfirmWindow();
            //phoneConfirmWindow.ShowDialog();            
            
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
            SettingsPage page = new SettingsPage(refreshDelegate);
            PageNavigator.Content = page;
        }

        private void rbAboutUs_Click(object sender, RoutedEventArgs e)
        {
            AboutUsPage aboutUsPage = new AboutUsPage();
            PageNavigator.Content = aboutUsPage;
        }

        //private void rbFaq_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void rbHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpPage helpPage = new HelpPage();
            PageNavigator.Content = helpPage;
        }

        
        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();            
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

        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await refreshAsync();
        }

       

        

        private void btShoppingChart(object sender, RoutedEventArgs e)
        {
            ShoppingChartPage shoppingChartPage = new ShoppingChartPage();
            PageNavigator.Content = shoppingChartPage;
        }
    }
}
