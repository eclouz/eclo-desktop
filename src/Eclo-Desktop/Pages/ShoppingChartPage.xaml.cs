using Eclo_Desktop.Components.ShoppingCharts;
using Eclo_Desktop.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingChartPage.xaml
    /// </summary>

    public partial class ShoppingChartPage : Page
    {
        //Price ni update qilib turish uchun Delegat yartib olyabmiz
        public delegate void UpdateTotalPriceDelegate(double totalPrice, int intValue);
        public delegate void RefreshShoppingChart();
        public ShoppingChartPage(UpdateShoppingChartCountDelegate updateShoppingChartCount)
        {
            InitializeComponent();

            //Delegatga funksiyani bog'lash
            updateTotalPriceDelegate = UpdateTotalPrice;
            refreshShoppingChart = RefreshPageHandler;
            this._updateShoppingChartCount = updateShoppingChartCount;

        }
        // Delegatni oz'garuvchi yaratish
        public UpdateTotalPriceDelegate updateTotalPriceDelegate;
        public RefreshShoppingChart refreshShoppingChart;
        private UpdateShoppingChartCountDelegate _updateShoppingChartCount;

        //Delegat bog'lanadigan  funksiya
        private void UpdateTotalPrice(double totalPrice, int intValue)
        {
            // O'zgaruvchilarni qabul qiling
            // tbTotalPrice ni o'zgartirish logikasi
            tbTotalPrice.Text = (totalPrice).ToString("0.00") + " so'm";
        }



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
            foreach (var item in List)
            {
                //Delegatni UserControlga konstruktor orqali berib yuborayapmiz
                Item charts = new Item(updateTotalPriceDelegate, refreshShoppingChart, _updateShoppingChartCount);
                charts.SetData(item);
                sPanelCharts.Children.Add(charts);

            }
            loader.Visibility = Visibility.Collapsed;

        }
        public async void RefreshPageHandler()
        {
            await RefreshAsync();
        }
    }
}
