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
            NewItemsUserControl newItemsUserControl = new NewItemsUserControl();
            MainWP.Children .Add(newItemsUserControl);
            NewItemsUserControl newItemsUserControl2 = new NewItemsUserControl();
            MainWP.Children.Add(newItemsUserControl2);
            NewItemsUserControl newItemsUserControl3 = new NewItemsUserControl();
            MainWP.Children.Add(newItemsUserControl3);
            NewItemsUserControl newItemsUserControl4 = new NewItemsUserControl();
            MainWP.Children.Add(newItemsUserControl4);
            NewItemsUserControl newItemsUserControl5 = new NewItemsUserControl();
            MainWP.Children.Add(newItemsUserControl5);
            NewItemsUserControl newItemsUserControl6 = new NewItemsUserControl();
            MainWP.Children.Add(newItemsUserControl6);
        }
    }
}
