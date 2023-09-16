using Eclo.Domain.Entities.Products;
using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Eclo_Desktop.Utilities;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly IProductService _productService;

        public Dashboard()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

       
        //public async Task setDataForDashboard()
        //{
        //    await refreshAsync();
        //}
        //public async Task RefreshWithLike()
        //{
        //    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
        //    productLightClothesUserControl.RefreshDashboard = refreshAsync;
            
        //}
        public async Task refreshAsync()
        {
            //await RefreshWithLike();

            var identity = IdentitySingleton.GetInstance();
            SecondWp.Children.Clear();            
            var products = await _productService.GetAllProducts(identity.UserId, 1);
            foreach ( var product in products )
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);
                productLightClothesUserControl.RefreshPage = RefreshPageHandler;
            }

        }
        private async void RefreshPageHandler()
        {
            await refreshAsync();
        }
        
        private async void rbMens_Click_1(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);
            List<string> subCategoryName = new List<string>();
            SecondWp.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId , "Men", min, max, subCategoryName, 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                
                SecondWp.Children.Add(productLightClothesUserControl);
                
            }
        }

        private async void rbWomens_Click(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);
            List<string> subCategoryName = new List<string>();
            SecondWp.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);

            }
        }

        private async void rbKids_Click(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);
            List<string> subCategoryName = new List<string>();
            SecondWp.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", min, max, subCategoryName, 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);

            }
        }

        private async void rbAll_Click(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            SecondWp.Children.Clear();
            var products = await _productService.GetAllProducts(identity.UserId, 1);
            foreach (var product in products)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);

            }
        }

        private void btnShowMeAll_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage());
        }

        private void btnShowMeAll2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage());
        }

        private void btnShowMeAll3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DiscountsPage());
        }

        private void btnShowMeAll4_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DiscountsPage());
        }

        private async void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            await refreshAsync();
            //ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
            //SecondWp.Children.Add(productLightClothesUserControl);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
