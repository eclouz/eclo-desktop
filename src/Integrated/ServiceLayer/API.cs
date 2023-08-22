using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer;

public class API
{
    public static readonly string BASE_URL = "http://eclo.uz:8080/api/";
    public static readonly string CREATE_USER = BASE_URL + "auth/register";
    public static readonly string SEND_CODE_REGISTER = BASE_URL + "auth/register/send-code";
}
