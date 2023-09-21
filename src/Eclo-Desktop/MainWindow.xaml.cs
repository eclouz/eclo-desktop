using Eclo.Domain.Entities.Categories;
using Eclo_Desktop.Pages;
using Eclo_Desktop.Security;
using Eclo_Desktop.Themes;
using Eclo_Desktop.Windows;
using Integrated.ServiceLayer.Categories;
using Integrated.ServiceLayer.Categories.Concrete;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
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
    public delegate void RefreshPageHandlerDelegate(Page page);

    public partial class MainWindow : Window
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IUserService _userService;

        private readonly RefreshPageHandlerDelegate refreshDelegate;

        public MainWindow()
        {
            InitializeComponent();
            this._userService = new UserService();
            this._subCategoryService = new SubCategoryService();
            refreshDelegate = RefreshPageHandler;
        }
        public async  Task refreshAsync(Page page)
        {
            var identity = IdentitySingleton.GetInstance();
            var result = await _userService.GetUserById(identity.Token);

            lblUserName.Content = result.FirstName;
            lblCountry.Content = result.Region;

            string imageUrl = "https://localhost:7190/" + result.ImagePath;
            Uri uri = new Uri(imageUrl, UriKind.Absolute);
            brUserImage.ImageSource = new BitmapImage(uri);


            //Dashboard dashboard = new Dashboard();
            PageNavigator.Content = page;
        }
        public async void RefreshPageHandler(Page page)
        {
            await refreshAsync(page);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            await refreshAsync(dashboard);

            
        }

        private async void rbDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            PageNavigator.Content = dashboard;
            //PageNavigator.Navigate(new Uri("Men.xaml", UriKind.Relative));
            dashboard.cbSubCategories.Items.Clear();
            var identity = IdentitySingleton.GetInstance();
            var subCategories = await _subCategoryService.GetAllSubCategories(1);

            for (int i = 0; i < subCategories.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategories[i].Name;

                dashboard.cbSubCategories.Items.Add(checkBox);
            }
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
            Dashboard dashboard = new Dashboard();
            await refreshAsync(dashboard);
        }

       

        

        private void btShoppingChart(object sender, RoutedEventArgs e)
        {
            ShoppingChartPage shoppingChartPage = new ShoppingChartPage();
            PageNavigator.Content = shoppingChartPage;
        }
    }
}
