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
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Men", min, max, subCategoryName, 1);

            wpMens.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                wpMens.Children.Add(productLightClothesUserControl);
            }
            subCategoryName = subCategoryName.Distinct().ToList();

            cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
            loader.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductsPage());
        }

        private async void bApply_Click(object sender, RoutedEventArgs e)
        {
            var loaderButtton = bApply.Template.FindName("loader", bApply) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loaderButtton!.Visibility = Visibility.Visible;

            //button to disable
            bApply.IsEnabled = false;

            loader.Visibility = Visibility.Visible;
            if (int.Parse(tbMin.Text) <= int.Parse(tbMax.Text))
            {
                int min = int.Parse(tbMin.Text);
                int max = int.Parse(tbMax.Text);
                if (min == 0) min += 1;
                List<string> subCategoryName = new List<string>();

                foreach (var item in cbSubCategories.Items)
                {
                    if (item is CheckBox checkbox && checkbox.IsChecked == true)
                    {
                        subCategoryName.Add(checkbox.Content.ToString());
                    }
                }

                var identity = IdentitySingleton.GetInstance();
                var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Men", min, max, subCategoryName, 1);



                wpMens.Children.Clear();
                for (int i = 0; i < mensCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl();
                    productLightClothesUserControl.setData(mensCategoryProducts[i]);
                    wpMens.Children.Add(productLightClothesUserControl);
                }
            }
            loader.Visibility = Visibility.Collapsed;

            // For the Loader to stop
            loaderButtton.Visibility = Visibility.Collapsed;

            // button to enable
            bApply.IsEnabled = true;
        }
    }
}
    