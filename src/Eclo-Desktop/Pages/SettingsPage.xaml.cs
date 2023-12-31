﻿using Eclo.DataAccess.ViewModels.Users;
using Eclo_Desktop.Security;
using Integrated.ServiceLayer;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using Integrated.Validation;
using Microsoft.Win32;
using Notification.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Eclo_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        IUserService userService = new UserService();
        UserViewModel userViewModel = new UserViewModel();
        ValidationAttribute validationAttribute = new ValidationAttribute();
        private RefreshPageHandlerDelegate refreshDelegate;

        public SettingsPage(RefreshPageHandlerDelegate refreshPageHandlerDelegate)
        {
            InitializeComponent();
            this.refreshDelegate = refreshPageHandlerDelegate;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var identity = IdentitySingleton.GetInstance();
            var getUserInfo = await userService.GetUserById(identity.Token);
            tbName.Text = getUserInfo?.FirstName;
            tbSecondName.Text = getUserInfo?.LastName;
            tbPassportSerialNumber.Text = getUserInfo?.PassportSerialNumber;
            //if (DateBirthdp.SelectedDate is not null) { getUserInfo.BirthDate = DateBirthdp.SelectedDate.Value; }

            string imageUrl = API.BASE_URL_IMAGE + getUserInfo?.ImagePath;
            Uri imageUri = new Uri(imageUrl, UriKind.Absolute);
            UserImage.ImageSource = new BitmapImage(imageUri);
            loader.Visibility = Visibility.Collapsed;
            tbRegion.Text = getUserInfo?.Region;
            tbDistric.Text = getUserInfo?.District;
            tbAdress.Text = getUserInfo?.Address;


        }
        private async void btnSaveSettingsChange_Click(object sender, RoutedEventArgs e)
        {

            DateTime? selectedDate = DateBirthdp.SelectedDate;
            var loader = btnSaveSettingsChange.Template.FindName("loader", btnSaveSettingsChange) as FontAwesome.WPF.ImageAwesome;
            loader.Visibility = Visibility.Visible;
            btnSaveSettingsChange.IsEnabled = false;

            if (validationAttribute.IsValidName(tbName.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidName(tbName.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            else if (validationAttribute.IsValidSurname(tbSecondName.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidSurname(tbSecondName.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            else if (validationAttribute.IsValidPassport(tbPassportSerialNumber.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidPassport(tbPassportSerialNumber.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            //else if (selectedDate.HasValue && validationAttribute.IsValidDate(selectedDate.Value.ToString("dd/MM/yyyy")).isSuccessful == false)
            //{
            //    var notificationManager = new NotificationManager();
            //    notificationManager.Show("Warning!", validationAttribute.IsValidDate(selectedDate.Value.ToString("dd/MM/yyyy")).Message, NotificationType.Warning);
            //    loader.Visibility = Visibility.Collapsed;
            //    btnSaveSettingsChange.IsEnabled = true;
            //}




            else if (validationAttribute.IsValidRegion(tbRegion.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidRegion(tbRegion.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            else if (validationAttribute.IsValidDistrict(tbDistric.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidDistrict(tbDistric.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            else if (validationAttribute.IsValidAdress(tbAdress.Text).isSuccessful == false)
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("Warning!", validationAttribute.IsValidAdress(tbAdress.Text).Message, NotificationType.Warning);
                loader.Visibility = Visibility.Collapsed;
                btnSaveSettingsChange.IsEnabled = true;
            }
            else
            {
                try
                {
                    var identity = IdentitySingleton.GetInstance();
                    var getUserInfo = await userService.GetUserById(identity.Token);

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

                    var updateUserInfo = await userService.UserUpdateSettings(userViewModel, identity.Token);
                    if (updateUserInfo == true)
                    {
                        loader.Visibility = Visibility.Collapsed;
                        btnSaveSettingsChange.IsEnabled = true;

                        //For Notification Update Successful
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Successful!", "Update account", NotificationType.Success, RowsCountWhenTrim: 2);

                        SettingsPage settingsPage = new SettingsPage(refreshDelegate);
                        refreshDelegate(settingsPage);
                    }
                    else
                    {
                        loader.Visibility = Visibility.Collapsed;
                        btnSaveSettingsChange.IsEnabled = true;
                        //For Notification Update Warning
                        var notificationManager = new NotificationManager();
                        notificationManager.Show("Warning!", "Please try again", NotificationType.Warning, RowsCountWhenTrim: 2);
                    }
                }
                catch
                {
                    loader.Visibility = Visibility.Collapsed;
                    btnSaveSettingsChange.IsEnabled = true;

                    //For Notification Update Warning
                    var notificationManager = new NotificationManager();
                    notificationManager.Show("Warning!", "Please try again", NotificationType.Warning, RowsCountWhenTrim: 2);
                }
            }
        }

        private void updateImage_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() == true)
            {
                string imgpath = openfiledialog.FileName;
                UserImage.ImageSource = new BitmapImage(new Uri(imgpath, UriKind.Relative));
            }

        }

    }
}
