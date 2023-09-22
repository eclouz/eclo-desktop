using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for SmallProductPicturesUserControl.xaml
    /// </summary>
    public partial class SmallProductPicturesUserControl : UserControl
    {
        public string ColorName { get; set; }
        public Uri uri { get; set; }
        public Func<string, Uri, Task> SetDataToComponent { get; set; }
        public SmallProductPicturesUserControl()
        {
            InitializeComponent();
        }

        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await SetDataToComponent(ColorName, uri);
        }
        public void setData(string ColorName, Uri uri)
        {
            this.ColorName = ColorName;
            this.uri = uri;
            brLittlePictureBorder.ImageSource = new BitmapImage(uri);

        }
    }
}
