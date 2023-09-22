using Dtos.Product;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for SizeUserControl.xaml
    /// </summary>
    public partial class SizeUserControl : UserControl
    {

        ProductGetSizeDto ProductGetSizeDtoMain2 { get; set; }
        public Func<ProductGetSizeDto, Task> ProductGetSize { get; set; }
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
