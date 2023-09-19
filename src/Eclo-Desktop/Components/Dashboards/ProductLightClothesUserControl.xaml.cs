using Eclo_Desktop.Components.Products;
using Eclo_Desktop.Security;
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
        
        private IProductService _productService;
        //public bool Liked { get; set; } 
        IProductService productService = new ProductService();
        public ProductLightClothesUserControl()
        {
            InitializeComponent();
            this._productService = new ProductService();
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnQuickView_Click(object sender, RoutedEventArgs e)
        {
            string token = IdentitySingleton.GetInstance().Token;
            
            QuickView1Window quickView1Window = new QuickView1Window();
            var identity = IdentitySingleton.GetInstance();
            var result = await productService.GetByIdProducts(identity.UserId, productViewModels.Id,token);            
            quickView1Window.setData(result);                            
            quickView1Window.ShowDialog();
        }

        //public Func<Task> RefreshDashboard { get ; set; }
        public delegate void RefreshDelegate();
        public RefreshDelegate RefreshPage { get; set; }
        private async void brLike_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //await RefreshDashboard();
            string token = IdentitySingleton.GetInstance().Token;
            int page = 1;
            var getUserProductLikesList = await productService.getUserProductLikes(page,token);
            var identity = IdentitySingleton.GetInstance();
            string pathRedLike = "Assets\\StaticImages\\like.png";

            for (int i = 0; i < getUserProductLikesList.Count; i++)
            {
                if (getUserProductLikesList[i].productId == productViewModels.Id && getUserProductLikesList[i].userId == identity.UserId
                    && getUserProductLikesList[i].isLiked == true)
                {
                    //Oq like                
                    brLike.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\love.png", UriKind.Relative));
                    var likeUpdate = await productService.UserProductLikeUpdate(getUserProductLikesList[i].Id, 
                        identity.UserId, productViewModels.Id, false,token);
                    
                    if (likeUpdate == true)
                    {
                        //MessageBox.Show("Removed from cart");
                    }
                    else
                    {
                        //MessageBox.Show("Not Removed from cart. Something wrong 🥱");
                    }
                    break;
                }
                else if (getUserProductLikesList[i].productId == productViewModels.Id && getUserProductLikesList[i].userId == identity.UserId
                    && getUserProductLikesList[i].isLiked == false)
                {
                    // Qizil like
                    brLike.ImageSource = new BitmapImage(new System.Uri(pathRedLike, UriKind.Relative));
                    var likeUpdate = await productService.UserProductLikeUpdate(getUserProductLikesList[i].Id, 
                        identity.UserId, productViewModels.Id, true, token);

                    var identity2 = IdentitySingleton.GetInstance();
                    
                    if (likeUpdate == true)
                    {
                        //MessageBox.Show("Successfully saved to savelist");
                    }
                    else
                    {
                        //MessageBox.Show("Not Saved to cart. Something wrong 🥱");
                    }
                    break;
                }
                if(i == getUserProductLikesList.Count-1)
                {
                    var identity2 = IdentitySingleton.GetInstance();
                    var likeIt = await _productService.UserSetLikeTrue(identity2.UserId, productViewModels.Id, true);
                    // Qizil like
                    brLike.ImageSource = new BitmapImage(new System.Uri(pathRedLike, UriKind.Relative));

                    if (likeIt == true)
                    {
                        //MessageBox.Show("Successfully saved to savelist");
                    }
                    else
                    {
                        //MessageBox.Show("Not Saved to cart. Something wrong 🥱");
                    }
                    break;
                }
            }
            if (getUserProductLikesList.Count==0)
            {
                var identity2 = IdentitySingleton.GetInstance();
                var likeIt = await _productService.UserSetLikeTrue(identity2.UserId, productViewModels.Id, true);
                // Qizil like
                brLike.ImageSource = new BitmapImage(new System.Uri(pathRedLike, UriKind.Relative));
               
                if (likeIt == true)
                {
                    //MessageBox.Show("Successfully saved to savelist");
                }
                else
                {
                    //MessageBox.Show("Not Saved to cart. Something wrong 🥱");
                }
            }

            RefreshPage?.Invoke();

        }
        public async void setData(ProductViewModels productViewModels)
        {            
            lblClotheName.Content = productViewModels.ProductName;            
            foreach ( var i in productViewModels.ProductDetail)
            {
                lblClotheColorDescription.Content = i.Color;
                string imageUrl = "https://localhost:7190/" + i.ImagePath;
                Uri imageUri = new Uri(imageUrl, UriKind.Absolute);                
                imgProduct.ImageSource = new BitmapImage(imageUri);
            }
            //viewModel.Id = productViewModels.Id;
            this.productViewModels.Id = productViewModels.Id;
            this.productViewModels.ProductLiked = productViewModels.ProductLiked;
            this.productViewModels.likedId = productViewModels.likedId;
            this.productViewModels.ProductDetail=productViewModels.ProductDetail;

            string pathRedLike = "Assets\\StaticImages\\like.png";
            if (productViewModels.ProductLiked==true)
            {
                brLike.ImageSource = new BitmapImage(new System.Uri(pathRedLike, UriKind.Relative));
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\love.png", UriKind.Relative));                
            }

            lblPproductPrice.Content = (productViewModels.ProductPrice).ToString();
                   
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int countItem = 0;
            foreach (var item in this.productViewModels.ProductDetail)
            {
                if (countItem == 5) 
                {
                    break;
                }
                countItem++;
                SmallProductPicturesUserControl smallProductPicturesUserControl = new SmallProductPicturesUserControl();
                string imageUrl = "https://localhost:7190/" + item.ImagePath;
                Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
                smallProductPicturesUserControl.setData(item.Color,imageUri);
                smallProductPicturesUserControl.SetDataToComponent = setDataToComponent;
                SPLittlePictures.Children.Add(smallProductPicturesUserControl);
            }

        }
        public async Task setDataToComponent(string colorName,Uri imageUri)
        {
            lblClotheColorDescription.Content = colorName;
            imgProduct.ImageSource = new BitmapImage(imageUri);

        }
    }
}
