using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModels.Products;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Women_sCollection.xaml
    /// </summary>
    public partial class Women_sCollection : Page
    {
        private readonly IProductService _productService;
        private readonly UpdateShoppingChartCountDelegate _updateShoppingChartCount;

        List<ProductViewModels> roductViewModelsList { get; set; }

        public Women_sCollection(UpdateShoppingChartCountDelegate updateShoppingChart)
        {
            InitializeComponent();
            this._productService = new ProductService();
            this._updateShoppingChartCount = updateShoppingChart;

        }

        public async Task refreshAsync()
        {
          
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);


            wpWomen.Children.Clear();
            loader.Visibility = Visibility.Visible;
            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                wpWomen.Children.Add(productLightClothesUserControl);
            }
            loader.Visibility = Visibility.Collapsed;
            subCategoryName = subCategoryName.Distinct().ToList();

            cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
            

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);

            wpWomen.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                wpWomen.Children.Add(productLightClothesUserControl);
            }
            loader.Visibility = Visibility.Collapsed;
            subCategoryName = subCategoryName.Distinct().ToList();

            cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProductsPage(_updateShoppingChartCount));
        }

        private async void bApply_Click(object sender, RoutedEventArgs e)
        {
           

            var loaderButtton = bApply.Template.FindName("loader", bApply) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loaderButtton!.Visibility = Visibility.Visible;


            //button to disable
            bApply.IsEnabled = false;

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
                var womensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);



                wpWomen.Children.Clear();
                loader.Visibility = Visibility.Visible;

                for (int i = 0; i < womensCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                    productLightClothesUserControl.setData(womensCategoryProducts[i]);
                    wpWomen.Children.Add(productLightClothesUserControl);
                }
                loader.Visibility = Visibility.Collapsed;
            }
            // For the Loader to stop
            loaderButtton.Visibility = Visibility.Collapsed;

            // button to enable
            bApply.IsEnabled = true;

            loader.Visibility = Visibility.Collapsed;
        }
    }
}
