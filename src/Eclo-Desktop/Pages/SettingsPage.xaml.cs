using Eclo.DataAccess.ViewModels.Users;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        IUserService userService = new UserService();
        UserViewModel userViewModel = new UserViewModel();
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var getUserInfo = await userService.GetUserById(identity.UserId);
            tbName.Text = getUserInfo?.FirstName;
            tbSecondName.Text = getUserInfo?.LastName;
            tbPassportSerialNumber.Text = getUserInfo?.PassportSerialNumber;
            //if (DateBirthdp.SelectedDate is not null) { getUserInfo.BirthDate = DateBirthdp.SelectedDate.Value; }

            string imageUrl = "http://eclo.uz:8080/" + getUserInfo?.ImagePath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            UserImage.ImageSource = new BitmapImage(imageUri);
            tbRegion.Text = getUserInfo?.Region;
            tbDistric.Text = getUserInfo?.District;
            tbAdress.Text = getUserInfo?.Address;


        }
        private async void btnSaveSettingsChange_Click(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var getUserInfo = await userService.GetUserById(identity.UserId);
            
            userViewModel.FirstName = tbName.Text;
            userViewModel.LastName = tbSecondName.Text;
            userViewModel.PassportSerialNumber = tbPassportSerialNumber.Text;
            if (DateBirthdp.SelectedDate is not null) { userViewModel.BirthDate = DateBirthdp.SelectedDate.Value; }
            userViewModel.Region = tbRegion.Text;
            userViewModel.District = tbDistric.Text;
            userViewModel.Address = tbAdress.Text;
            userViewModel.PhoneNumber = getUserInfo.PhoneNumber;

            string image_path = UserImage.ImageSource.ToString();
            if (!String.IsNullOrEmpty(image_path))
            {
                userViewModel.ImagePath = image_path;
            }

            var updateUserInfo = await userService.UserUpdateSettings(userViewModel);
            if(updateUserInfo==true)
            {
                MessageBox.Show("Your informations are updated");

            }
            else
            {
                MessageBox.Show("SomeThing wrong Try Again");
            }


        }
        //private async Task<string> CopyImageAsync(string imgPath, string destinationDirectory)
        //{
        //    if (!Directory.Exists(destinationDirectory))
        //        Directory.CreateDirectory(destinationDirectory);

        //    var imageName = ContentNameMaker.GetImageName(imgPath);

        //    string path = System.IO.Path.Combine(destinationDirectory, imageName);

        //    byte[] image = await File.ReadAllBytesAsync(imgPath);

        //    await File.WriteAllBytesAsync(path, image);

        //    return path;
        //}

        private void updateImage_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() == true)
            {
                string imgpath = openfiledialog.FileName;
                UserImage.ImageSource = new BitmapImage(new Uri(imgpath, UriKind.Relative));
            }

        }

        private void btnSaveSecuritySettingsChange_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Service is Currently in work Please wait");
        }
    }
}
