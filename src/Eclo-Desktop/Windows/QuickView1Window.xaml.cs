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
using static Eclo_Desktop.Components.Products.PictureProductUserControl;
using Dtos.Product;

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
            //if(liked==false)
            //{
            //    brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));
            //    liked = true;
            //}
            //else
            //{
            //    brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));
            //    liked = false;
            //}
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
            
        public List<ProductGetSizeDto> productGetSizeListForPublish { get; set; }
        public async Task setDataToMainImage(string  image,List<Uri> extraPictures,List<ProductGetSizeDto> sizeList, string color)
        {
            productGetSizeListForPublish = sizeList;
            lblColor.Content = color;
            Uri imageUri = new Uri(image, UriKind.Absolute);
            imageQuickview.ImageSource = new BitmapImage(imageUri);
            if (extraPictures.Count == 0)
            {
                imageQuickview2.ImageSource = new BitmapImage(imageUri);
                imageQuickview3.ImageSource = new BitmapImage(imageUri);
            }
            if(extraPictures.Count > 0)
            {                
                   if (extraPictures.Count > 1)
                    {
                    imageQuickview2.ImageSource = new BitmapImage(extraPictures[1]);
                    imageQuickview3.ImageSource = new BitmapImage(extraPictures[0]);
                        
                    }
                    if (extraPictures.Count==1)
                    {
                        imageQuickview3.ImageSource = new BitmapImage(extraPictures[0]);
                    imageQuickview2.ImageSource = new BitmapImage(imageUri);
                }                
            }
            // Getting Size to Window
            await refreshSizeAsync();                
        }
        public async Task setDataToMainImage2(ProductGetSizeDto dto)
        {
            lblQuantity.Content = dto.quantity.ToString();
            lblSize.Content = dto.Size.ToString();
        }
        public async Task refreshSizeAsync()
        {
            wpProductSizes.Children.Clear();
            foreach (var sizes in productGetSizeListForPublish)
            {
                SizeUserControl sizeUserControl = new SizeUserControl();
                sizeUserControl.setData(sizes);
                wpProductSizes.Children.Add(sizeUserControl);
                sizeUserControl.ProductGetSize = setDataToMainImage2;

            }



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
                brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\minus.png"));
                brDescription2.Visibility = Visibility.Visible;
                isDescripitonPressed=true;
            }
            else
            {

                brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\add (2).png"));
                brDescription2.Visibility = Visibility.Collapsed;
                isDescripitonPressed = false;
            }
        }
        public async void setData(ProductGetViewModel productGetViewModel)
        {
            // Thisvariable for counting reviews
            int countComments = 0;
                                    
            lblProductName.Content = productGetViewModel.ProductName;
            foreach (var i in productGetViewModel.ProductDetail)
            {
                lblColor.Content = i.Color;
                string imageUrl = "https://localhost:7190/" + i.ImagePath;
                Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
                imageQuickview.ImageSource = new BitmapImage(imageUri);

                List< ProductGetSizeDto> productGetSizeList = new List< ProductGetSizeDto>();
                List<Uri> ExtraImages = new List<Uri>();
                
                foreach (var item in i.ProductDetailFashions)
                {
                    string extraUrl = "https://localhost:7190/" + item.ImagePath;
                    Uri extraImageUri = new Uri(extraUrl, UriKind.Absolute);
                    ExtraImages.Add(extraImageUri);
                }
                foreach(var size in i.ProductDetailSizes)
                {
                    ProductGetSizeDto productGetSizeDto = new ProductGetSizeDto();
                    productGetSizeDto.Id = size.Id;
                    productGetSizeDto.Size = size.Size;
                    productGetSizeDto.quantity = size.Quantity;
                    productGetSizeList.Add(productGetSizeDto);

                }                
                

                if (ExtraImages.Count>0)
                {
                    if (ExtraImages.Count > 1)
                    {                        
                        imageQuickview3.ImageSource = new BitmapImage(ExtraImages[1]);
                        imageQuickview2.ImageSource = new BitmapImage(ExtraImages[0]);
                    }
                    if (ExtraImages.Count == 1)
                    {
                        imageQuickview2.ImageSource = new BitmapImage(ExtraImages[0]);
                        imageQuickview2.ImageSource = new BitmapImage(imageUri);
                    }
                }
                
                
                // pictures products
                PictureProductUserControl pictureProductUserControl = new PictureProductUserControl();
                pictureProductUserControl.setData(imageUri,ExtraImages, productGetSizeList,i.Color);                               
                wpProductPictures.Children.Add(pictureProductUserControl);

                pictureProductUserControl.SetImageToMainPicture = setDataToMainImage;


            }

            // comments
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

        private void tbComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbComment.Text.Length > 0)
            {
                brSendComment.Visibility= Visibility.Visible;   
            }
            else
            {
                brSendComment.Visibility = Visibility.Collapsed;
            }

        }

        private void brSendComment_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
