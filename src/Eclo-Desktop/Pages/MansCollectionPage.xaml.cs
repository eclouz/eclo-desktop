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
    /// Interaction logic for MansCollectionPage.xaml
    /// </summary>
    public partial class MansCollectionPage : Page
    {
        public bool clicked { get; set; } = false;
        public MansCollectionPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!clicked)
            //{
            //    brCategories.Visibility = Visibility.Visible;
            //    clicked = true;
            //}
            //else
            //{
            //    brCategories.Visibility = Visibility.Collapsed;
            //    clicked = false;
            //}
        }
        

    }
}
