using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
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
using ViewModels.Products;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Women_sCollection.xaml
    /// </summary>
    public partial class Women_sCollection : Page
    {
        private readonly IProductService _productService;
        List<ProductViewModels> roductViewModelsList { get ; set; }

        public Women_sCollection()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        public async Task refreshAsync()
        {
            wpWomen.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                wpWomen.Children.Add(productLightClothesUserControl);

            }

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await refreshAsync();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductsPage());
        }
    }
}
