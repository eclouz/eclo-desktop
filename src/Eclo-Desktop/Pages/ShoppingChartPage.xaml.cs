using Eclo_Desktop.Components.ShoppingCharts;
using Eclo_Desktop.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ShoppingChartPage.xaml
    /// </summary>
    
    public partial class ShoppingChartPage : Page
    {
        public delegate void UpdateTotalPriceDelegate(double totalPrice, int intValue);
        public ShoppingChartPage()
        {
            InitializeComponent();
            // Delegatni yaratish
            updateTotalPriceDelegate = UpdateTotalPrice;
        }

        private void UpdateTotalPrice(double totalPrice, int intValue)
        {
            // O'zgaruvchilarni qabul qiling
            // tbTotalPrice ni o'zgartirish logikasi
            tbTotalPrice.Text = (totalPrice).ToString("0.00") + " so'm";
        }

        // Delegatni o'zlashtirish
        public UpdateTotalPriceDelegate updateTotalPriceDelegate;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            await RefreshAsync();

        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                
            }
        }
        public async Task RefreshAsync()
        {
            sPanelCharts.Children.Clear();

            var identity = IdentitySingleton.GetInstance();            

            var List = identity.ShoppingChartProducts;
            foreach(var item in List)
            {
                Item charts = new Item(updateTotalPriceDelegate);
                charts.SetData(item);
                sPanelCharts.Children.Add(charts);

            }
            
        }
    }
}
