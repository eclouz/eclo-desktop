using Dtos.Product;
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
    /// Interaction logic for SizeUserControl.xaml
    /// </summary>
    public partial class SizeUserControl : UserControl
    {

         ProductGetSizeDto ProductGetSizeDtoMain2 { get ; set; } 
        public Func<ProductGetSizeDto,Task> ProductGetSize { get; set; }
        public SizeUserControl()
        {
            InitializeComponent();
        }
        public void setData(ProductGetSizeDto dto)
        {
            ProductGetSizeDtoMain2 = dto;
            lblSizeSpace.Content = dto.Size;
        }

        private async void SizeButton_Click(object sender, RoutedEventArgs e)
        {
            await ProductGetSize(ProductGetSizeDtoMain2);
        }
    }
}
