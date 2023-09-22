using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Categories;
using Integrated.ServiceLayer.Categories.Concrete;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        public readonly UpdateShoppingChartCountDelegate _upateShoppingChartCount;

        public Dashboard(UpdateShoppingChartCountDelegate updateShoppingChartCount)
        {
            InitializeComponent();
            this._productService = new ProductService();
            this._subCategoryService = new SubCategoryService();
            this._upateShoppingChartCount = updateShoppingChartCount;
        }
        public Dashboard()
        {
            InitializeComponent();
            this._productService = new ProductService();
            this._subCategoryService = new SubCategoryService();
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

            loader.Visibility = Visibility.Visible;

            var identity = IdentitySingleton.GetInstance();
            SecondWp.Children.Clear();
            int page = int.Parse(tbPage.Text);
            var result = await _productService.GetAllProducts(identity.UserId, page);
            identity.pagination = result.pageData;
            if (result.pageData.TotalPages < 2)
            {
                tbPage.Visibility = Visibility.Collapsed;
                tbTotalPage.Visibility = Visibility.Collapsed;
                tbbackslash.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Collapsed;
                btnPervouce.Visibility = Visibility.Collapsed;
            }
            tbTotalPage.Text = result.pageData.TotalPages.ToString();
            var products = result.productViewModels;
            loader.Visibility = Visibility.Collapsed;
            foreach (var product in products)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);
                productLightClothesUserControl.RefreshPage = RefreshPageHandler;
            }

            cbSubCategories.Items.Clear();
            var subCategories = await _subCategoryService.GetAllSubCategories(1);

            List<string> subCategoryName = new List<string>();
            for (int i = 0; i < subCategories.Count; i++)
            {
                subCategoryName.Add(subCategories[i].Name);
            }

            subCategoryName = subCategoryName.Distinct().ToList();

            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
            loader.Visibility = Visibility.Collapsed;
        }
        private async void RefreshPageHandler()
        {
            await refreshAsync();
        }

        private async void rbMens_Click_1(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Men", min, max, subCategoryName, 1);

            SecondWp.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                SecondWp.Children.Add(productLightClothesUserControl);
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

        private async void rbWomens_Click(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);

            SecondWp.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                SecondWp.Children.Add(productLightClothesUserControl);
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

        private async void rbKids_Click(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", min, max, subCategoryName, 1);

            SecondWp.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                SecondWp.Children.Add(productLightClothesUserControl);
            }
            subCategoryName = subCategoryName.Distinct().ToList();

            cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
            loader.Visibility=Visibility.Collapsed;
        }

        private async void rbAll_Click(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;
            var identity = IdentitySingleton.GetInstance();
            SecondWp.Children.Clear();
            int page = int.Parse(tbPage.Text);
            var result = await _productService.GetAllProducts(identity.UserId, page);
            identity.pagination = result.pageData;
            var products = result.productViewModels;
            foreach (var product in products)
            {
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                productLightClothesUserControl.setData(product);
                SecondWp.Children.Add(productLightClothesUserControl);
            }

            cbSubCategories.Items.Clear();
            var subCategories = await _subCategoryService.GetAllSubCategories(1);
            List<string> subCategoryName = new List<string>();
            for (int i = 0; i < subCategories.Count; i++)
            {
                subCategoryName.Add(subCategories[i].Name);
            }
            subCategoryName = subCategoryName.Distinct().ToList();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                cbSubCategories.Items.Add(checkBox);
            }
            loader.Visibility = Visibility.Collapsed;
        }

        private void btnShowMeAll_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage(_upateShoppingChartCount));
        }

        private void btnShowMeAll2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage(_upateShoppingChartCount));
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

        private async void bApply_Click(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;
            var loaderButtton = bApply.Template.FindName("loader", bApply) as FontAwesome.WPF.ImageAwesome;

            // For the Loader to run
            loaderButtton!.Visibility = Visibility.Visible;
            //button to disable
            bApply.IsEnabled = false;

            if (rbAll.IsChecked == true && int.Parse(tbMin.Text) <= int.Parse(tbMax.Text))
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
                var allCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "All", min, max, subCategoryName, 1);



                SecondWp.Children.Clear();
                for (int i = 0; i < allCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                    productLightClothesUserControl.setData(allCategoryProducts[i]);
                    SecondWp.Children.Add(productLightClothesUserControl);
                }
            }
            else if (rbMens.IsChecked == true && int.Parse(tbMin.Text) <= int.Parse(tbMax.Text))
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



                SecondWp.Children.Clear();
                for (int i = 0; i < mensCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                    productLightClothesUserControl.setData(mensCategoryProducts[i]);
                    SecondWp.Children.Add(productLightClothesUserControl);
                }
            }
            else if (rbWomens.IsChecked == true && int.Parse(tbMin.Text) <= int.Parse(tbMax.Text))
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
                var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);



                SecondWp.Children.Clear();
                for (int i = 0; i < mensCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                    productLightClothesUserControl.setData(mensCategoryProducts[i]);
                    SecondWp.Children.Add(productLightClothesUserControl);
                }
            }
            else if (rbKids.IsChecked == true && int.Parse(tbMin.Text) <= int.Parse(tbMax.Text))
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
                var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", min, max, subCategoryName, 1);



                SecondWp.Children.Clear();
                for (int i = 0; i < mensCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_upateShoppingChartCount);
                    productLightClothesUserControl.setData(mensCategoryProducts[i]);
                    SecondWp.Children.Add(productLightClothesUserControl);
                }
            }
            loader.Visibility = Visibility.Collapsed;
            // For the Loader to stop
            loaderButtton.Visibility = Visibility.Collapsed;

            // button to enable
            bApply.IsEnabled = true;
        }
        private async void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            var loader_button = btnPervouce.Template.FindName("loader", btnPervouce) as FontAwesome.WPF.ImageAwesome;
            loader_button!.Visibility = Visibility.Visible;
            loader.Visibility = Visibility.Visible;
            btnPervouce.IsEnabled = false;
            int page = int.Parse(tbPage.Text);
            if (page > 1)
            {
                page -= 1;
                tbPage.Text = page.ToString();
                await refreshAsync();
            }
            loader.Visibility = Visibility.Collapsed;
            loader_button.Visibility = Visibility.Collapsed;
            btnPervouce.IsEnabled = true;

        }


        private async void NextPage_Click(object sender, RoutedEventArgs e)
        {
            var loader_button = btnNext.Template.FindName("loader", btnNext) as FontAwesome.WPF.ImageAwesome;
            loader_button!.Visibility = Visibility.Visible;
            loader.Visibility = Visibility.Visible;

            btnPervouce.IsEnabled = false;
            var identity = IdentitySingleton.GetInstance();
            int page = int.Parse(tbPage.Text);
            if (page < identity.pagination.TotalPages)
            {
                page += 1;
                tbPage.Text = page.ToString();
                await refreshAsync();
            }
            loader.Visibility = Visibility.Collapsed;
            loader_button.Visibility = Visibility.Collapsed;
            btnPervouce.IsEnabled = true;

        }
    }
}
