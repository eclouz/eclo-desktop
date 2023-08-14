﻿using System;
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

namespace Eclo_Desktop.Components.Dashboards
{
    /// <summary>
    /// Interaction logic for NewItemsUserControl.xaml
    /// </summary>
    public partial class NewItemsUserControl : UserControl
    {
        public bool Liked { get; set; } = false;
        public NewItemsUserControl()
        {
            InitializeComponent();
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuickView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void brLike_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Liked)
            {             
                brLike.ImageSource = new BitmapImage(new  System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\like.png", UriKind.Relative));                        
                Liked = true;
            }
            else
            {
                brLike.ImageSource = new BitmapImage(new System.Uri("C:\\Users\\hasan\\OneDrive\\Рабочий стол\\Current_Working_Project\\eclo-desktop\\src\\Eclo-Desktop\\Assets\\StaticImages\\love.png", UriKind.Relative));                
                Liked = false;
            }

        }
    }
}
