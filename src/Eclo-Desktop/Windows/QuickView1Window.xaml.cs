﻿using Dtos.Comment;
using Dtos.Product;
using Eclo_Desktop.Components.Products;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer;
using Integrated.ServiceLayer.Comments;
using Integrated.ServiceLayer.Comments.Concrete;
using Integrated.ServiceLayer.Product;
using Integrated.ServiceLayer.Product.Concrete;
using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ViewModels.Products;
using ViewModels.ShoppingCharts;

namespace Eclo_Desktop.Windows;

/// <summary>
/// Interaction logic for QuickView1Window.xaml
/// </summary>
public partial class QuickView1Window : Window
{
    private bool isDescripitonPressed { get; set; } = false;
    private string imagePath { get; set; }
    private long productId { get; set; }
    public long productGetViewId;

    private IProductService productService = new ProductService();
    private ICommentService _commentService;
    public UpdateShoppingChartCountDelegate _upateShoppingChartCount;
    public List<ProductGetSizeDto> productGetSizeListForPublish { get; set; }

    public QuickView1Window(UpdateShoppingChartCountDelegate updateShoppingChartCount)
    {
        InitializeComponent();
        this._upateShoppingChartCount = updateShoppingChartCount;
        this._commentService = new CommentService();
    }

    //Start======Window Loaded========================================================================================================== 
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        lblReviewCount2.Content = lblReviewCount.Content;

    }
    //End======Window Loaded==========================================================================================================       

    //Start======Close Button========================================================================================================== 
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    //End======Close Button========================================================================================================== 

    //Start===========Buttons Minus and Plus===========================================================================================================
    private void btnMinus_Click(object sender, RoutedEventArgs e)
    {
        int productCount = int.Parse(lblItemCount.Text);
        if (productCount > 1)
        {
            productCount -= 1;
            lblItemCount.Text = productCount.ToString();
        }
    }
    private void btnPlus_Click(object sender, RoutedEventArgs e)
    {
        int productCount = int.Parse(lblItemCount.Text);
        if (productCount < int.Parse(lblQuantity.Content.ToString()!))
        {
            productCount += 1;
            lblItemCount.Text = productCount.ToString();
        }
    }
    //End===========Buttons Minus and Plus==============================================================================================================

    //Start======Description Border Mouse Down========================================================================================================== 
    private void brDescription_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!isDescripitonPressed)
        {
            brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\minus.png", UriKind.Relative));
            brDescription2.Visibility = Visibility.Visible;
            isDescripitonPressed = true;
        }
        else
        {

            brPlusForDescription.ImageSource = new BitmapImage(new System.Uri("Assets\\StaticImages\\add (2).png", UriKind.Relative));
            brDescription2.Visibility = Visibility.Collapsed;
            isDescripitonPressed = false;
        }
    }
    //End======Description Border Mouse Down========================================================================================================== 

    //Start======Comment TextBox TextCHanged========================================================================================================== 
    private void tbComment_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (tbComment.Text.Length > 0)
        {
            brSendComment.Visibility = Visibility.Visible;
        }
        else
        {
            brSendComment.Visibility = Visibility.Collapsed;
        }

    }
    //End======Comment TextBox TextCHanged========================================================================================================== 

    //Start======Comment Send Comment MouseDown========================================================================================================== 
    private async void brSendComment_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (tbComment.Text.Length > 0 && tbComment.Text != null)
        {
            brSendComment.IsEnabled = false;
            var identity = IdentitySingleton.GetInstance();
            CommentDto commentDto = new CommentDto()
            {
                ProductId = productId,
                UserId = identity.UserId,
                Comment = tbComment.Text.ToString(),
                IsEdited = true
            };


            // For register Send request 
            bool response = await _commentService.CreateComment(commentDto, identity.Token);
            brSendComment.IsEnabled = true;
            tbComment.Text = "";
            refreshCommentAsync();
        }
        else
        {
            // For Comment Error Notification
            var notificationManager = new NotificationManager();
            notificationManager.Show("Warning!", "Comment not empty", NotificationType.Warning);
        }
    }
    //End======Comment Send Comment MouseDown========================================================================================================== 

    //Start======AddCart  Button========================================================================================================== 
    private void btAddCart(object sender, RoutedEventArgs e)
    {
        if (lblProductName.Content != null && lblColor.Content != null && lblSize.Content != null && lblItemCount.Text != null
            && tbDescription.Text != null && lblPrice.Content != null && imagePath != null)
        {
            Guid newGuid = Guid.NewGuid();
            ShoppingChartViewModel shoppingChartViewModel = new ShoppingChartViewModel()
            {

                Id = newGuid.ToString(),
                ProductName = lblProductName.Content.ToString()!,
                ProductColor = lblColor.Content.ToString()!,
                ProductSize = lblSize.Content.ToString()!,
                ProductQuantity = int.Parse(lblQuantity.Content.ToString()!),
                ItemCount = int.Parse(lblItemCount.Text!),
                ProductDescription = tbDescription.Text.ToString(),
                ProductPrice = double.Parse(lblPrice.Content.ToString()!),
                ProductImage = imagePath,
                ProductDiscount = 10,

            };
            var identity = IdentitySingleton.GetInstance();

            var List = identity.ShoppingChartProducts;
            List.Add(shoppingChartViewModel);
            identity.ShoppingChartProducts = List;

            //For Save product ShoppingChart Success
            var notificationManager = new NotificationManager();
            notificationManager.Show("Success!", "Save product ShoppingChart", NotificationType.Success, RowsCountWhenTrim: 2);

            _upateShoppingChartCount();

        }
        else
        {
            //For Save product ShoppingChart Warning
            var notificationManager = new NotificationManager();
            notificationManager.Show("Warning!", "Please try again", NotificationType.Warning, RowsCountWhenTrim: 2);
        }


    }
    //End======AddCart  Button========================================================================================================== 

    public void setData(ProductGetViewModel productGetViewModel, long Id)
    {
        productGetViewId = Id;
        // This variable for counting reviews
        int countComments = 0;
        productId = productGetViewModel.Id;
        //Product Name
        lblProductName.Content = productGetViewModel.ProductName;


        foreach (var productDetail in productGetViewModel.ProductDetail)
        {
            //For ImagePath Field
            imagePath = productDetail.ImagePath;
            //Product Color
            lblColor.Content = productDetail.Color;

            //Product Main Image
            string imageUrl = API.BASE_URL_IMAGE + productDetail.ImagePath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            imageQuickview.ImageSource = new BitmapImage(imageUri);

            List<ProductGetSizeDto> productGetSizeList = new List<ProductGetSizeDto>();
            List<Uri> ExtraImages = new List<Uri>();

            foreach (var item in productDetail.ProductDetailFashions)
            {
                string extraUrl = API.BASE_URL_IMAGE + item.ImagePath;
                Uri extraImageUri = new Uri(extraUrl, UriKind.Absolute);
                ExtraImages.Add(extraImageUri);
            }
            foreach (var size in productDetail.ProductDetailSizes)
            {
                ProductGetSizeDto productGetSizeDto = new ProductGetSizeDto();
                productGetSizeDto.Id = size.Id;
                productGetSizeDto.Size = size.Size;
                productGetSizeDto.quantity = size.Quantity;
                productGetSizeList.Add(productGetSizeDto);

            }


            if (ExtraImages.Count > 0)
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
            pictureProductUserControl.setData(imageUri, ExtraImages, productGetSizeList, productDetail.Color);
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
        tbDescription.Text = (productGetViewModel.ProductDescription).ToString();

    }
    public async void refreshCommentAsync()
    {
        string token = IdentitySingleton.GetInstance().Token;

        QuickView1Window quickView1Window = new QuickView1Window(_upateShoppingChartCount);
        var identity = IdentitySingleton.GetInstance();
        var result = await productService.GetByIdProducts(identity.UserId, productGetViewId, token);

        // comments
        foreach (var i in result.ProductComments)
        {
            ReviewsUserControl reviewsUserControl = new ReviewsUserControl();
            reviewsUserControl.setData(i.UserId, i.Comment, (i.CreatedAt).ToString());
            ReviewsWp.Children.Add(reviewsUserControl);
        }

        lblReviewCount.Content = lblReviewCount2.Content;
    }
    public async Task setDataToMainImage(string image, List<Uri> extraPictures, List<ProductGetSizeDto> sizeList, string color)
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
        if (extraPictures.Count > 0)
        {
            if (extraPictures.Count > 1)
            {
                imageQuickview2.ImageSource = new BitmapImage(extraPictures[1]);
                imageQuickview3.ImageSource = new BitmapImage(extraPictures[0]);

            }
            if (extraPictures.Count == 1)
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

}
