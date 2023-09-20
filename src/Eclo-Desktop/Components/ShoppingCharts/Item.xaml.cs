using Eclo_Desktop.Security;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ViewModels.ShoppingCharts;
using static Eclo_Desktop.Pages.ShoppingChartPage;

namespace Eclo_Desktop.Components.ShoppingCharts
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        string Id;
        private int productQuantity = 0;
        private double productPrice;
        private UpdateTotalPriceDelegate updatePriceDelgate;
        private RefreshShoppingChart refreshPage;

        public Item(UpdateTotalPriceDelegate updateTotalPriceDelegate, RefreshShoppingChart refreshShoppingChart)
        {
            InitializeComponent();
            //Fieldga konstruktor orqali kirib kelayotgan delegat olayabmiz
            this.updatePriceDelgate = updateTotalPriceDelegate;
            this.refreshPage = refreshShoppingChart;
        }

       

        private async void btProductDelete(object sender, RoutedEventArgs e)
        {
            IdentitySingleton identity = IdentitySingleton.GetInstance();
            var products = identity.ShoppingChartProducts;
            identity.ShoppingChartProducts= products.Where(x => x.Id != Id).ToList();
            if (chbSelect.IsChecked == true)
            {
                int productCount = int.Parse(tbProductQuantity.Text);

                identity.TotalPrice -= productPrice*productCount;

                //Delegatga qiymatlarni berib yuboryapmiz
                Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, int.Parse(tbProductQuantity.Text.ToString())));
            }
            refreshPage();

        }
        public async void SetData(ShoppingChartViewModel shoppingChartView)
        {
            tbProductName.Text = shoppingChartView.ProductName;
            tbDescription.Text = shoppingChartView.ProductDescription;
            tbProductColor.Text = shoppingChartView.ProductColor;
            tbProductPrice.Text = shoppingChartView.ProductPrice.ToString() + " so'm";
            tbProductQuantity.Text = shoppingChartView.ItemCount.ToString();
            tbProductSize.Text = shoppingChartView.ProductSize.ToString();
            tbQuantity.Text = "Quantity: " + shoppingChartView.ProductQuantity.ToString();
            string imageUrl = "https://localhost:7190/" + shoppingChartView.ProductImage;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            imageProduct.ImageSource = new BitmapImage(imageUri);
            productQuantity = shoppingChartView.ProductQuantity;
            productPrice = shoppingChartView.ProductPrice;
            Id = shoppingChartView.Id;

        }

        private async void plus_Click(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();

            if (chbSelect.IsChecked == true)
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount < productQuantity)
                {
                    productCount += 1;
                    tbProductQuantity.Text = productCount.ToString();

                    identity.TotalPrice += productPrice;
                    //Delegatni qiymatlarni berib yuboryapmiz
                    Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, (int)identity.TotalPrice));
                }

               
            }
            else
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount < productQuantity)
                {
                    productCount += 1;
                    tbProductQuantity.Text = productCount.ToString();
                }
            }

        }

        private async void minus_Click(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            if (chbSelect.IsChecked == true)
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount > 1)
                {
                    productCount -= 1;
                    tbProductQuantity.Text = productCount.ToString();

                    identity.TotalPrice -= productPrice;

                    //Delegatga qiymatlarni berib yuboryapmiz
                    Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, int.Parse(tbProductQuantity.Text.ToString())));
                }
            }
                
            else
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount > 1)
                {
                    productCount -= 1;
                    tbProductQuantity.Text = productCount.ToString();
                }
            }

        }



        private async void chbSelect_Checked(object sender, RoutedEventArgs e)
        {

            var identity = IdentitySingleton.GetInstance();


            identity.TotalPrice += productPrice * int.Parse(tbProductQuantity.Text);
            //Delegatni qiymatlarni berib yuboryapmiz
            Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, (int)identity.TotalPrice));



        }

        private async void chbSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();

            identity.TotalPrice -= productPrice * int.Parse(tbProductQuantity.Text); ;

            //Delegatga qiymatlarni berib yuboryapmiz
            Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, int.Parse(tbProductQuantity.Text.ToString())));


        }
    }

}
