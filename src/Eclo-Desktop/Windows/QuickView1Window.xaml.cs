﻿using Eclo_Desktop.Components.Products;
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

namespace Eclo_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for QuickView1Window.xaml
    /// </summary>
    public partial class QuickView1Window : Window
    {
        private bool liked { get; set; } =  false;
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
            SizeUserControl sizeUserControl = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl);SizeUserControl sizeUserControl2 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl2);SizeUserControl sizeUserControl3 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl3);SizeUserControl sizeUserControl4 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl4);SizeUserControl sizeUserControl5 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl5);SizeUserControl sizeUserControl6 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl6);SizeUserControl sizeUserControl7 = new SizeUserControl();
            SPSize.Children.Add(sizeUserControl7);
        }

        private void LeftBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RightBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}