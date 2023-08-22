using Dtos.Auth;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer;

public class API
{
    //Base Url
    public static readonly string BASE_URL = "http://eclo.uz:8080/api/";
    
    //Login Register Confirm Password URLs
    public static readonly string CREATE_USER = BASE_URL + "auth/register";
    public static readonly string SEND_CODE_REGISTER = BASE_URL + "auth/register/send-code";
    public static readonly string VERIFY_REGISTER = BASE_URL + "auth/register/verify";
    public static readonly string LOGIN = BASE_URL + "auth/login";
    //Login Register Confirm Password URLs

    //Brand Urls
    public static readonly string COMMON_BRANDS = BASE_URL + "common/brands";
    //Brand Urls
}
