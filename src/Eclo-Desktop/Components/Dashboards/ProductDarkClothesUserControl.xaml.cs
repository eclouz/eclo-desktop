using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop.Components.Dashboards
{
    /// <summary>
    /// Interaction logic for ProductDarkClothesUserControl.xaml
    /// </summary>
    public partial class ProductDarkClothesUserControl : UserControl
    {
        public bool Liked { get; set; } = false;
        public ProductDarkClothesUserControl()
        {
            InitializeComponent();
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuickView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Liked)
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\like.png", UriKind.Relative));
                Liked = true;
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\love.png", UriKind.Relative));
                Liked = false;
            }

        }
    }
}
