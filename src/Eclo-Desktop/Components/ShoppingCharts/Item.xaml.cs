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

namespace Eclo_Desktop.Components.ShoppingCharts
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
        }
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(Item));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private async void plus_Click(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();

            if (chbSelect.IsChecked == true) 
            {
                identity.TotalPrice -= productPrice * int.Parse(tbProductQuantity.Text);

                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount < productQuantity)
                {
                    productCount += 1;
                    tbProductQuantity.Text = productCount.ToString();
                }
               
                identity.TotalPrice += productPrice * int.Parse(tbProductQuantity.Text);
                //Delegatni qiymatlarni berib yuboryapmiz
                Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, (int)identity.TotalPrice));
            }
            else
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount < productQuantity)
                {
                    productCount += 1;
                    tbProductQuantity.Text = productCount.ToString();
                }
            }
           
        }


        public string Ref
        {
            var identity = IdentitySingleton.GetInstance();
            if (chbSelect.IsChecked == true)
            {
                identity.TotalPrice += productPrice * int.Parse(tbProductQuantity.Text); ;
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount > 1)
                {
                    productCount -= 1;
                    tbProductQuantity.Text = productCount.ToString();
                }

               

                identity.TotalPrice -= productPrice * int.Parse(tbProductQuantity.Text); ;

                //Delegatga qiymatlarni berib yuboryapmiz
                Dispatcher.Invoke(() => updatePriceDelgate(identity.TotalPrice, int.Parse(tbProductQuantity.Text.ToString())));
            }
            else
            {
                int productCount = int.Parse(tbProductQuantity.Text);
                if (productCount > 1)
                {
                    productCount -= 1;
                    tbProductQuantity.Text = productCount.ToString();
                }
            }
           
        }

        public static readonly DependencyProperty RefProperty = DependencyProperty.Register("Ref", typeof(string), typeof(Item));


        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(string), typeof(Item));


        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(string), typeof(Item));


        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(string), typeof(Item));
    }
}
