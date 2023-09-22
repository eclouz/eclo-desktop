using Eclo.Domain.Entities.Discounts;
using System.Windows.Controls;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for DiscountsUserControl.xaml
    /// </summary>
    public partial class DiscountsUserControl : UserControl
    {

        public DiscountsUserControl()
        {
            InitializeComponent();
        }

        public void setData(Discount discount)
        {
            lblDiscountName.Content = discount.Name;
            lblDiscountPersentage.Content = discount.Percentage + "%";
            tbDescription.Text = discount.Description;
        }
    }
}
