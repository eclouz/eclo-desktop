using Eclo_Desktop.Pages;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer;
using Integrated.ServiceLayer.Categories;
using Integrated.ServiceLayer.Categories.Concrete;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void RefreshPageHandlerDelegate(Page page);
    public delegate void UpdateShoppingChartCountDelegate();
    public partial class MainWindow : Window
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IUserService _userService;

        private readonly RefreshPageHandlerDelegate refreshDelegate;
        public UpdateShoppingChartCountDelegate updateShoppingChartCount;
        public MainWindow()
        {
            InitializeComponent();
            this._userService = new UserService();
            this._subCategoryService = new SubCategoryService();
            refreshDelegate = RefreshPageHandler;
            updateShoppingChartCount = UpdateShoppingChart;
        }
        public async Task refreshAsync(Page page)
        {
            var identity = IdentitySingleton.GetInstance();
            var result = await _userService.GetUserById(identity.Token);

            lblUserName.Content = result.FirstName;
            lblCountry.Content = result.Region;

            string imageUrl = API.BASE_URL_IMAGE + result.ImagePath;
            Uri uri = new Uri(imageUrl, UriKind.Absolute);
            brUserImage.ImageSource = new BitmapImage(uri);
            loaderUserImage.Visibility = Visibility.Collapsed;

            //Dashboard dashboard = new Dashboard();
            PageNavigator.Content = page;
        }
        public async void RefreshPageHandler(Page page)
        {
            await refreshAsync(page);
        }

        // For Update ShoppingChart Icon new Order
        public void UpdateShoppingChart()
        {
            lbCount.Text = IdentitySingleton.GetInstance().ShoppingChartProducts.Count.ToString();
            if (int.Parse(lbCount.Text) == 0)
            {
                basketCount.Visibility = Visibility.Collapsed;
            }
            else
            {
                basketCount.Visibility = Visibility.Visible;
            }
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard(updateShoppingChartCount);
            await refreshAsync(dashboard);
        }

        private async void rbDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard(updateShoppingChartCount);
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
            ProductsPage page = new ProductsPage(updateShoppingChartCount);
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
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
            SaveListPage saveListPage = new SaveListPage(updateShoppingChartCount);
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Dashboard dashboard = new Dashboard(updateShoppingChartCount);
            //await refreshAsync(dashboard);
        }
        private void btShoppingChart(object sender, RoutedEventArgs e)
        {
            ShoppingChartPage shoppingChartPage = new ShoppingChartPage(updateShoppingChartCount);
            PageNavigator.Content = shoppingChartPage;
        }
    }
}
