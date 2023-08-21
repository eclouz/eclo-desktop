using Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer.User;

 public interface IUserService
{
    Task<bool> CreateUser(RegisterDto registerDto);
}
