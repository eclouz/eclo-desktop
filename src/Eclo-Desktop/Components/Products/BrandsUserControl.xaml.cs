using Integrated.ServiceLayer;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
