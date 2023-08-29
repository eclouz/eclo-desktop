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
    /// Interaction logic for SmallProductPicturesUserControl.xaml
    /// </summary>
    public partial class SmallProductPicturesUserControl : UserControl
    {
        public string ColorName {  get ; set; }
        public Uri uri { get; set; }
        public Func<string,Uri,Task> SetDataToComponent { get; set; }
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
