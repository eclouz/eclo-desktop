using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Components.Products;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Brand;
using Integrated.ServiceLayer.Brand.Concrete;
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
using ViewModels.Brands;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for MansCollectionPage.xaml
    /// </summary>
    public partial class MansCollectionPage : Page
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;

        public bool clicked { get; set; } = false;
        public MansCollectionPage()
        {
            InitializeComponent();
            this._brandService = new BrandService();
            this._productService = new ProductService();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //int min = int.Parse(tbMin.Text);
            //int max = int.Parse(tbMax.Text);
            List<string> subCategoryName = new List<string>();
            wpMens.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Men", 0, 9999999, subCategoryName, 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                wpMens.Children.Add(productLightClothesUserControl);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductsPage());
        }
    }
}
    