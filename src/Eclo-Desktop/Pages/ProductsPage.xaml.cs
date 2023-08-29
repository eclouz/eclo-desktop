using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
        }

        private void MensCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MansCollectionPage());
        }

        private void WomensCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Women_sCollection());
        }

        private void KidsCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Kid_sCollection());
        }
    }
}
