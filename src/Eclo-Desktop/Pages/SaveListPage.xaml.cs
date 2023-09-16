using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for SaveListPage.xaml
    /// </summary>
    public partial class SaveListPage : Page
    {

        private readonly IProductService _productService;
        public SaveListPage()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            await refreshAsync();

        }
        public async Task refreshAsync()
        {
            MainWP123.Children.Clear();
            var identity = IdentitySingleton.GetInstance();
            var product = await _productService.GetAllProducts(identity.UserId, 1); 
            loader.Visibility = Visibility.Collapsed;

            foreach (var productItem in product)
            {
                if (productItem.ProductLiked == true)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                    productLightClothesUserControl.setData(productItem);
                    MainWP123.Children.Add(productLightClothesUserControl);
                    productLightClothesUserControl.RefreshPage = RefreshPageHandler;
                }
            }
          
        }
        private async void RefreshPageHandler()
        {
            await refreshAsync();
        }

    }
}
