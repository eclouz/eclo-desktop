using Eclo_Desktop.Components.Products;
using Integrated.ServiceLayer.Product.Concrete;
using Integrated.ServiceLayer.Product;
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
using System.Windows.Shapes;
using ViewModels.Products;

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for QuickView1Window.xaml
    /// </summary>
    public partial class QuickView1Window : Window
    {
        private bool isDescripitonPressed { get; set; } = false;
        IProductService productService = new ProductService();
        
        private bool liked { get; set; } =  false;
        int count = 0;
        public QuickView1Window()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(liked==false)
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
                liked = true;
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));
                liked = false;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {                                    
            lblReviewCount2.Content = lblReviewCount.Content;   
        }

        private void LeftBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RightBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if(0>int.Parse(lblItemCount.Text.ToString()))
            {
                lblItemCount.Text = 0.ToString();
            }

            if(0!=int.Parse(lblItemCount.Text.ToString()) && 0< int.Parse(lblItemCount.Text.ToString()))
            {
                count--;
                lblItemCount.Text = count.ToString();
            }
            
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            
            count++;
            lblItemCount.Text = count.ToString();

        }

        private void brDescription_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!isDescripitonPressed)
            {
                brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\minus.png"));
                brDescription2.Visibility = Visibility.Visible;
                isDescripitonPressed=true;
            }
            else
            {
                brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\add (2).png"));
                brDescription2.Visibility= Visibility.Collapsed;
                isDescripitonPressed=false;
            }
        }
        public async void setData(ProductGetViewModel productGetViewModel)
        {
            int countComments = 0;
            //var getProductById = await productService.GetByIdProducts(id);
            
            lblProductName.Content = productGetViewModel.ProductName;
            foreach (var i in productGetViewModel.ProductDetail)
            {
                lblColor.Content = i.Color;
                string imageUrl = "http://eclo.uz:8080/" + i.ImagePath;
                Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
                imageQuickview.ImageSource = new BitmapImage(imageUri);
                
            }
            foreach (var i in productGetViewModel.ProductComments)
            {
                countComments++;
                ReviewsUserControl reviewsUserControl = new ReviewsUserControl();
                reviewsUserControl.setData(i.UserId, i.Comment, (i.CreatedAt).ToString());
                ReviewsWp.Children.Add(reviewsUserControl); 
            }
            lblReviewCount2.Content = countComments.ToString();
            lblReviewCount.Content = lblReviewCount2.Content;
            lblPrice.Content = (productGetViewModel.ProductPrice).ToString();
            tbDescription.Text=(productGetViewModel.ProductDescription).ToString();


        }
    }
}
