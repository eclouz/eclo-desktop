using Eclo.Domain.Entities.Discounts;
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
