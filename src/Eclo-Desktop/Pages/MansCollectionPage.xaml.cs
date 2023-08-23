using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Components.Products;
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

        public bool clicked { get; set; } = false;
        public MansCollectionPage()
        {
            InitializeComponent();
            this._brandService = new BrandService();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BrandsUserControl brandsUserControl = new BrandsUserControl();
            //SPSize.Children.Add(brandsUserControl);
            var brands = await _brandService.GetAllBrands(1);
            foreach (var brend in brands)
            {
                BrandsUserControl brandsUserControl = new BrandsUserControl();
                brandsUserControl.setData(brend);
                SPSize.Children.Add(brandsUserControl);

            }
        }

    }
}
    