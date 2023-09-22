using Dtos.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for PictureProductUserControl.xaml
    /// </summary>
    public partial class PictureProductUserControl : UserControl
    {
        public List<Uri> productsImagePathList { get; set; }
        public List<ProductGetSizeDto> SizeList { get; set; }
        public string ProductColor { get; set; }

        public Func<string, List<Uri>, List<ProductGetSizeDto>, string, Task> SetImageToMainPicture { get; set; }
        public PictureProductUserControl()
        {
            InitializeComponent();
        }
        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            await SetImageToMainPicture((brPictures.ImageSource).ToString(), productsImagePathList, SizeList, ProductColor);

        }
        public void setData(Uri uri, List<Uri> list, List<ProductGetSizeDto> sizeList, string color)
        {
            brPictures.ImageSource = new BitmapImage(uri);
            productsImagePathList = list;
            SizeList = sizeList;
            ProductColor = color;
        }

    }
}
