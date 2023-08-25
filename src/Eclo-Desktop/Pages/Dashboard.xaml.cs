using Eclo_Desktop.Components.Dashboards;
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
            //SecondWp.Children.Add(productLightClothesUserControl);
            await refreshAsync();
        }
        public async Task refreshAsync()
        {
            SecondWp.Children.Clear();            
            var products = await _productService.GetAllProducts(1);
            foreach ( var product in products )
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);

            }

        }
    }
}
