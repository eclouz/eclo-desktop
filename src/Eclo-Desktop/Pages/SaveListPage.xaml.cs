using Eclo_Desktop.Components.Dashboards;
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

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for SaveListPage.xaml
    /// </summary>
    public partial class SaveListPage : Page
    {
        public SaveListPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProductLightClothesUserControl newItemsUserControl = new ProductLightClothesUserControl();
            newItemsUserControl.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl);
            ProductLightClothesUserControl newItemsUserControl2 = new ProductLightClothesUserControl();
            newItemsUserControl2.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl2);
            ProductLightClothesUserControl newItemsUserControl3 = new ProductLightClothesUserControl();
            newItemsUserControl3.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl3);
            ProductLightClothesUserControl newItemsUserControl4 = new ProductLightClothesUserControl();
            newItemsUserControl4.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl4);
            ProductLightClothesUserControl newItemsUserControl5 = new ProductLightClothesUserControl();
            newItemsUserControl5.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl5);
            ProductLightClothesUserControl newItemsUserControl6 = new ProductLightClothesUserControl();
            newItemsUserControl6.brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            MainWP.Children.Add(newItemsUserControl6);
        }
    }
}
