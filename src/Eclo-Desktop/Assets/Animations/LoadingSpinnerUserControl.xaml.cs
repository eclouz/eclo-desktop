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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eclo_Desktop.Assets.Animations
{
    /// <summary>
    /// Interaction logic for LoadingSpinnerUserControl.xaml
    /// </summary>
    public partial class LoadingSpinnerUserControl : UserControl
    {
        public LoadingSpinnerUserControl()
        {
            InitializeComponent();
            CreateLoaderAnimation();
        }

        private void CreateLoaderAnimation()
        {
            // Create the Ellipse
            var ellipse = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Gray // Customize the color as needed
            };

            // Add the Ellipse to the Canvas
            Canvas.Children.Add(ellipse);

            // Create the animation
            var storyboard = new Storyboard();
            var rotationAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(1)) // Adjust the duration as needed
            };
            Storyboard.SetTarget(rotationAnimation, ellipse);
            Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            storyboard.Children.Add(rotationAnimation);
            storyboard.Begin();
        }
    }
}
