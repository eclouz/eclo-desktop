using Eclo_Desktop.Components.Products;
using Integrated.ServiceLayer.IDiscountService;
using Integrated.ServiceLayer.IDiscountService.Concrete;
using System.Windows;
using System.Windows.Controls;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for DiscountsPage.xaml
    /// </summary>
    public partial class DiscountsPage : Page
    {
        private IDiscountService _discountService;
        public DiscountsPage()
        {
            InitializeComponent();
            this._discountService = new DiscountService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Dashboard());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var discountList = await _discountService.GetAllDiscounts(1);
            foreach (var item in discountList)
            {
                DiscountsUserControl discountsUserControl = new DiscountsUserControl();
                discountsUserControl.setData(item);
                wpDiscounts.Children.Add(discountsUserControl);

            }
        }
    }
}
