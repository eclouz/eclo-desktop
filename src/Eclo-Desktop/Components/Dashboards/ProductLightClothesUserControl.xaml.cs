using Eclo_Desktop.Windows;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using ViewModels.Products;

namespace Eclo_Desktop.Components.Dashboards
{
    /// <summary>
    /// Interaction logic for NewItemsUserControl.xaml
    /// </summary>
    public partial class ProductLightClothesUserControl : UserControl
    {
        ProductViewModels productViewModels = new ProductViewModels();
        public bool Liked { get; set; } = false;
        IProductService productService = new ProductService();
        public ProductLightClothesUserControl()
        {
            InitializeComponent();
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnQuickView_Click(object sender, RoutedEventArgs e)
        {
            QuickView1Window quickView1Window = new QuickView1Window();
            var result = await productService.GetByIdProducts(productViewModels.Id);            
            quickView1Window.setData(result);                            
            quickView1Window.ShowDialog();
        }

        private void brLike_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Liked)
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
                Liked = true;
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));
                Liked = false;
            }

        }
        public async void setData(ProductViewModels productViewModels)
            {            
            lblClotheName.Content = productViewModels.ProductName;            
            foreach ( var i in productViewModels.ProductDetail)
            {
                lblClotheColorDescription.Content = i.Color;
                string imageUrl = "http://eclo.uz:8080/" + i.ImagePath;
                Uri imageUri = new Uri(imageUrl, UriKind.Absolute);                
                imgProduct.ImageSource = new BitmapImage(imageUri);
            }
            //viewModel.Id = productViewModels.Id;
            this.productViewModels.Id = productViewModels.Id;
            lblPproductPrice.Content = (productViewModels.ProductPrice).ToString();
            

            if (productViewModels.ProductLiked==true)
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
                Liked = true;
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));
                Liked = false;
            }
        }

    }
}
