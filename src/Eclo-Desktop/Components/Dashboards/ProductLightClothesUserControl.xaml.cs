using Eclo_Desktop.Windows;
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
using ViewModels.Products;

namespace Eclo_Desktop.Components.Dashboards
{
    /// <summary>
    /// Interaction logic for NewItemsUserControl.xaml
    /// </summary>
    public partial class ProductLightClothesUserControl : UserControl
    {
        public bool Liked { get; set; } = false;
        public ProductLightClothesUserControl()
        {
            InitializeComponent();
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuickView_Click(object sender, RoutedEventArgs e)
        {
            QuickView1Window quickView1Window = new QuickView1Window();
            //setDataToQucikViewWindow
            quickView1Window.ShowDialog();
        }

        private void brLike_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Liked)
            {             
                //brLike.ImageSource = new BitmapImage(new  System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));                        
                //Liked = true;
            }
            else
            {
                //brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));                
                //Liked = false;
            }

        }
        public void setData(ProductViewModels productViewModels)
        {
            lblClotheName.Content = productViewModels.ProductName;
            lblClotheColorDescription.Content = productViewModels.ProductColor;
            lblPproductPrice.Content = productViewModels.ProductPrice;
            //lblRating.Content = productViewModels.
            if(productViewModels.ProductLiked==true)
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
