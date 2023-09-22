using Eclo_Desktop.Security;
using Integrated.ServiceLayer;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly IUserService _userService;

        public UserPage()
        {
            InitializeComponent();
            this._userService = new UserService();

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();

            var result = await _userService.GetUserById(identity.Token);


            string imageUrl = API.BASE_URL_IMAGE + result.ImagePath;
            Uri uri = new Uri(imageUrl, UriKind.Absolute);
            brProfileImage.ImageSource = new BitmapImage(uri);
            loader.Visibility = Visibility.Collapsed;

            lblName.Content = result.FirstName;
            lblSecondName.Content = result.LastName;
            lblPhoneNumber.Content = result.PhoneNumber;
            lblPassportSerialNUmber.Content = result.PassportSerialNumber;
            lblBirthDate.Content = result.BirthDate;
            lblDistrict.Content = result.District;
            lblAdress.Content = result.Address;

        }
    }
}
