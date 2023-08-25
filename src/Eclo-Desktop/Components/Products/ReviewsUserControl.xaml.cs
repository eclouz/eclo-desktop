using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels.Products;

namespace Eclo_Desktop.Components.Products
{
    /// <summary>
    /// Interaction logic for ReviewsUserControl.xaml
    /// </summary>
    public partial class ReviewsUserControl : UserControl
    {
        private readonly IUserService _userService;        
        public ReviewsUserControl()
        {
            InitializeComponent();
            _userService = new UserService();
        }
        public async void setData(long userId,string commentText,string dateTime)
        {
            var User = await _userService.GetUserById(userId);
            lblCommentOwer.Content = User.FirstName;
            tbCommentText.Text = commentText;
            lblReviewTime.Content = dateTime;
        }
    }
}
