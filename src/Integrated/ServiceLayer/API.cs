using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer;

public class API
{
    public static readonly string BASE_URL = "https://localhost:7190/api/";
    public static readonly string CREATE_USER = BASE_URL + "auth/register";
}
