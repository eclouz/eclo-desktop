﻿using Eclo_Desktop.Components.Dashboards;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private readonly IProductService _productService;
        public readonly UpdateShoppingChartCountDelegate _updateShoppingChartCount;
        //MansCollectionPage mansCollectionPage = new MansCollectionPage();
        //Women_sCollection women_SCollection = new Women_sCollection();
        //Kid_sCollection kid_SCollection=new Kid_sCollection();
        public ProductsPage(UpdateShoppingChartCountDelegate updateShoppingChartCount)
        {
            InitializeComponent();
            this._productService = new ProductService();
            this._updateShoppingChartCount = updateShoppingChartCount;

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            await refreshAsync();

        }

        private async void MensCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage(_updateShoppingChartCount));
            MansCollectionPage mansCollectionPage = new MansCollectionPage(_updateShoppingChartCount);
            mansCollectionPage.tbMin.Text = "2000";
            mansCollectionPage.tbMax.Text = "7500000";
            int min = int.Parse(mansCollectionPage.tbMin.Text);
            int max = int.Parse(mansCollectionPage.tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var mensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Men", min, max, subCategoryName, 1);

            mansCollectionPage.wpMens.Children.Clear();

            for (int i = 0; i < mensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(mensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                productLightClothesUserControl.setData(mensCategoryProducts[i]);
                mansCollectionPage.wpMens.Children.Add(productLightClothesUserControl);
            }
            subCategoryName = subCategoryName.Distinct().ToList();

            mansCollectionPage.cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                mansCollectionPage.cbSubCategories.Items.Add(checkBox);
            }

        }
        public async Task refreshAsync()
        {

        }
        private async void RefreshPageHandler()
        {
            await refreshAsync();
        }

        private async void WomensCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Women_sCollection(_updateShoppingChartCount));
            Women_sCollection women_SCollection = new Women_sCollection(_updateShoppingChartCount);
            women_SCollection.tbMin.Text = "2000";
            women_SCollection.tbMax.Text = "7500000";
            int min = int.Parse(women_SCollection.tbMin.Text);
            int max = int.Parse(women_SCollection.tbMax.Text);

            List<string> subCategoryName = new List<string>();

            var identity = IdentitySingleton.GetInstance();
            var womensCategoryProducts = await _productService.FilterBYCategories(identity.UserId, "Women", min, max, subCategoryName, 1);

            women_SCollection.wpWomen.Children.Clear();

            for (int i = 0; i < womensCategoryProducts.Count; i++)
            {
                subCategoryName.Add(womensCategoryProducts[i].SubCategory[0].Name);
                ProductLightClothesUserControl productLightClothesUserControl = new ProductLightClothesUserControl(_updateShoppingChartCount);
                productLightClothesUserControl.setData(womensCategoryProducts[i]);
                women_SCollection.wpWomen.Children.Add(productLightClothesUserControl);
            }
            subCategoryName = subCategoryName.Distinct().ToList();

            women_SCollection.cbSubCategories.Items.Clear();
            for (int i = 0; i < subCategoryName.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = subCategoryName[i];
                women_SCollection.cbSubCategories.Items.Add(checkBox);
            }
        }

        private void KidsCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Kid_sCollection(_updateShoppingChartCount));
        }
    }
}
