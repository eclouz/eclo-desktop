using Eclo_Desktop.Components.Products;
using Eclo_Desktop.Security;
using Eclo_Desktop.Windows;
using Integrated.ServiceLayer;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ViewModels.Products;

namespace Eclo_Desktop.Components.Dashboards;
/// <summary>
/// Interaction logic for NewItemsUserControl.xaml
/// </summary>


public partial class ProductLightClothesUserControl : UserControl
{
    private IProductService _productService;
    public UpdateShoppingChartCountDelegate _upateShoppingChartCount;

    ProductViewModels productViewModels = new ProductViewModels();
    IProductService productService = new ProductService();

    public delegate void RefreshDelegate();
    public RefreshDelegate RefreshPage { get; set; }
    
    public ProductLightClothesUserControl(UpdateShoppingChartCountDelegate updateShoppingChartCount)
    {
        InitializeComponent();
        this._productService = new ProductService();
        this._upateShoppingChartCount = updateShoppingChartCount;
    }
    private async void btnQuickView_Click(object sender, RoutedEventArgs e)
    {
        string token = IdentitySingleton.GetInstance().Token;

        QuickView1Window quickView1Window = new QuickView1Window(_upateShoppingChartCount);
        var identity = IdentitySingleton.GetInstance();
        var result = await productService.GetByIdProducts(identity.UserId, productViewModels.Id, token);
        quickView1Window.setData(result, productViewModels.Id);
        quickView1Window.ShowDialog();
    }
    public async void setData(ProductViewModels productViewModels)
    {
        loader.Visibility = Visibility.Visible;
        lblClotheName.Content = productViewModels.ProductName;
        foreach (var i in productViewModels.ProductDetail)
        {
            lblClotheColorDescription.Content = i.Color;
            string imageUrl = API.BASE_URL_IMAGE + i.ImagePath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            imgProduct.ImageSource = new BitmapImage(imageUri);

        }
        loader.Visibility = Visibility.Collapsed;
        //viewModel.Id = productViewModels.Id;
        this.productViewModels.Id = productViewModels.Id;
        this.productViewModels.ProductLiked = productViewModels.ProductLiked;
        this.productViewModels.likedId = productViewModels.likedId;
        this.productViewModels.ProductDetail = productViewModels.ProductDetail;

        //Make Price Format
        var numformat = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberGroupSizes = new int[] { 3 },
            NumberDecimalDigits = 0
        };

        if (productViewModels.ProductLiked == true)
        {
            btLike.Tag = "Red";
            btLike.Content = "Red";
        }
        else
        {
            btLike.Tag = "White";
            btLike.Content = "#323B4B";
        }
        if (productViewModels.ProductDiscount.Count > 0)
        {
            tbproductPriceRed.Visibility = Visibility.Visible;
            tbproductPriceRed.Text = (productViewModels.ProductPrice).ToString("N", numformat) + " so'm";

            var discount = productViewModels.ProductDiscount[productViewModels.ProductDiscount.Count - 1];
            var value = productViewModels.ProductPrice - ((productViewModels.ProductPrice / 100) * discount);

            tbProductPriceGreen.Text = value.ToString("N", numformat) + " so'm";

        }
        else
        {
            tbProductPriceGreen.Text = (productViewModels.ProductPrice).ToString("N", numformat) + " so'm";
        }

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
            string imageUrl = API.BASE_URL_IMAGE + item.ImagePath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            smallProductPicturesUserControl.setData(item.Color, imageUri);
            smallProductPicturesUserControl.SetDataToComponent = setDataToComponent;
            SPLittlePictures.Children.Add(smallProductPicturesUserControl);
            loader.Visibility = Visibility.Collapsed;
        }

    }
    public async Task setDataToComponent(string colorName, Uri imageUri)
    {
        lblClotheColorDescription.Content = colorName;
        imgProduct.ImageSource = new BitmapImage(imageUri);


    }

    private async void btLike_Click(object sender, RoutedEventArgs e)
    {
        //await RefreshDashboard();
        string token = IdentitySingleton.GetInstance().Token;
        int page = 1;
        var getUserProductLikesList = await productService.getUserProductLikes(page, token);
        var identity = IdentitySingleton.GetInstance();

        for (int i = 0; i < getUserProductLikesList.Count; i++)
        {
            if (getUserProductLikesList[i].productId == productViewModels.Id && getUserProductLikesList[i].userId == identity.UserId
                && getUserProductLikesList[i].isLiked == true)
            {
                //Oq like                
                btLike.Tag = "White";
                btLike.Content = "#323B4B";
                var likeUpdate = await productService.UserProductLikeUpdate(getUserProductLikesList[i].Id,
                    identity.UserId, productViewModels.Id, false, token);

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
                btLike.Tag = "Red";
                btLike.Content = "Red";
                var likeUpdate = await productService.UserProductLikeUpdate(getUserProductLikesList[i].Id,
                    identity.UserId, productViewModels.Id, true, token);               

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
            if (i == getUserProductLikesList.Count - 1)
            {
                var identity2 = IdentitySingleton.GetInstance();
                var likeIt = await _productService.UserSetLikeTrue(identity2.UserId, productViewModels.Id, identity2.Token, true);
                // Qizil like
                btLike.Tag = "Red";
                btLike.Content = "Red";

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
        if (getUserProductLikesList.Count == 0)
        {
            var identity2 = IdentitySingleton.GetInstance();
            var likeIt = await _productService.UserSetLikeTrue(identity2.UserId, productViewModels.Id, identity2.Token, true);
            // Qizil like
            btLike.Tag = "Red";
            btLike.Content = "Red";
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
}
