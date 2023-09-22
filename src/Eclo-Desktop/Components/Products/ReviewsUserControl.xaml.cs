using Eclo_Desktop.Security;
using Integrated.ServiceLayer.User;
using Integrated.ServiceLayer.User.Concrete;
using System.Windows.Controls;

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
        public async void setData(long userId, string commentText, string dateTime)
        {
            var identity = IdentitySingleton.GetInstance();
            var User = await _userService.GetUserById(identity.Token);
            lblCommentOwer.Content = User.FirstName;
            tbCommentText.Text = commentText;
            lblReviewTime.Content = dateTime;
        }
    }
}
