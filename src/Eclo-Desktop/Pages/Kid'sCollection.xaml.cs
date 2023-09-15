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

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Kid_sCollection.xaml
    /// </summary>
    public partial class Kid_sCollection : Page
    {
        private readonly IProductService _productService;
        public Kid_sCollection()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> subCategoryName = new List<string>();
            var identity = IdentitySingleton.GetInstance();
            wpKids.Children.Clear();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", 0, 9999999, subCategoryName, 1);
            foreach (var product in mensCategoryProducts)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                wpKids.Children.Add(productLightClothesUserControl);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductsPage());
        }
    }
}
