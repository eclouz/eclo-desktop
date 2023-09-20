using Dtos.Auth;
using Eclo.DataAccess.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer.User;

 public interface IUserService
{
    Task<bool> CreateUser(RegisterDto registerDto);
    Task<bool> SendCodeRegister(string phone);
    Task<(bool result, string token)> VerifyRegister(VerifyRegisterDto verifyRegisterDto);
    Task<(bool result, string token)> Login(LoginDto loginDto);
    Task<UserViewModel> GetUserById(string token);
    Task<UserViewModel> GetUserByPhoneNumber(string phone,string token);
    Task<bool> UserUpdateSettings(UserViewModel userViewModel,string token);
}
