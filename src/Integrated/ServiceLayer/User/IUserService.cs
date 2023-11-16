using Dtos.Auth;
using Eclo.DataAccess.ViewModels.Users;

namespace Integrated.ServiceLayer.User;

public interface IUserService
{
    Task<(bool result, string result_text)> CreateUser(RegisterDto registerDto);
    Task<bool> SendCodeRegister(string phone);
    Task<(bool result, string token)> VerifyRegister(VerifyRegisterDto verifyRegisterDto);
    Task<(bool result, string token)> Login(LoginDto loginDto);
    Task<UserViewModel> GetUserById(string token);
    Task<UserViewModel> GetUserByPhoneNumber(string phone, string token);
    Task<bool> UserUpdateSettings(UserViewModel userViewModel, string token);
}
