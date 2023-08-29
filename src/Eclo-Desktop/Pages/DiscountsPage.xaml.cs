using Eclo_Desktop.Components.Products;
using Integrated.ServiceLayer.IDiscountService;
using Integrated.ServiceLayer.IDiscountService.Concrete;
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
