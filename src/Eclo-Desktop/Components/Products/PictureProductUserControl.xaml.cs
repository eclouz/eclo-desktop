using Dtos.Product;
using Eclo_Desktop.Windows;
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
    /// Interaction logic for PictureProductUserControl.xaml
    /// </summary>
    public partial class PictureProductUserControl : UserControl
    {
        public List<Uri> productsImagePathList { get; set; }  
        public List<ProductGetSizeDto> SizeList { get; set; }
        public string ProductColor { get; set; }

        public Func<string,List<Uri>,List<ProductGetSizeDto>, string, Task> SetImageToMainPicture { get ; set; }
        public PictureProductUserControl()
        {
            InitializeComponent();
        }
        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            await SetImageToMainPicture((brPictures.ImageSource).ToString(),productsImagePathList,SizeList, ProductColor);

        }
        public void setData(Uri uri,List<Uri> list,List<ProductGetSizeDto> sizeList,string color)
        {
            brPictures.ImageSource = new BitmapImage(uri);
            productsImagePathList = list;
            SizeList = sizeList;
            ProductColor=color;
        }
        
    }
}
