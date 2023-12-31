﻿using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for Kid_sCollection.xaml
    /// </summary>
    public partial class Kid_sCollection : Page
    {
        private readonly IProductService _productService;
        private readonly UpdateShoppingChartCountDelegate _updateShoppingChartCount;

        public Kid_sCollection(UpdateShoppingChartCountDelegate updateShoppingChart)
        {
            InitializeComponent();
            this._productService = new ProductService();
            this._updateShoppingChartCount = updateShoppingChart;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbMin.Text = "2000";
            tbMax.Text = "7500000";
            int min = int.Parse(tbMin.Text);
            int max = int.Parse(tbMax.Text);

            List<string> subCategoryName = new List<string>();
            var identity = IdentitySingleton.GetInstance();
            var kidsCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", min, max, subCategoryName, 1);

            wpKids.Children.Clear();

            for (int i = 0; i < kidsCategoryProducts.Count; i++)
            {
                subCategoryName.Add(kidsCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                productLightClothesUserControl.setData(kidsCategoryProducts[i]);
                wpKids.Children.Add(productLightClothesUserControl);
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
            NavigationService?.Navigate(new ProductsPage(_updateShoppingChartCount));
        }

        private async void bApply_Click(object sender, RoutedEventArgs e)
        {
            loader.Visibility = Visibility.Visible;

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
                var kidsCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Kids", min, max, subCategoryName, 1);



                wpKids.Children.Clear();
                for (int i = 0; i < kidsCategoryProducts.Count; i++)
                {
                    ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                    productLightClothesUserControl.setData(kidsCategoryProducts[i]);
                    wpKids.Children.Add(productLightClothesUserControl);
                }
            }
            // For the Loader to stop
            loaderButtton.Visibility = Visibility.Collapsed;

            // button to enable
            bApply.IsEnabled = true;

            loader.Visibility = Visibility.Collapsed;
        }
    }
}
