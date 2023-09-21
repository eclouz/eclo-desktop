using Integrated.ServiceLayer;
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
using ViewModels.Brands;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for BrandsUserControl.xaml
    /// </summary>
    public partial class BrandsUserControl : UserControl
    {
        
        public BrandsUserControl()
        {
            InitializeComponent();
        }
        public void setData(MensBrandsViewModels mensBrandsViewModels)
        {
            string imageUrl = API.BASE_URL_IMAGE + mensBrandsViewModels.BrandIconPath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            ImgBrands.ImageSource = new BitmapImage(imageUri);
        }

    }
}
