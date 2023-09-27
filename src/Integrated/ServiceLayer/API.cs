namespace Integrated.ServiceLayer;

public static class API
{

    ////Base Url
    //public static readonly string BASE_URL = "http://68.183.181.125:89/api/";

    //public static readonly string BASE_URL_IMAGE = "http://68.183.181.125:89/";

    ////Base Url
    public static readonly string BASE_URL = "https://localhost:7190/api/";

    public static readonly string BASE_URL_IMAGE = "https://localhost:7190/";

    //Login Register Confirm Password URLs
    public static readonly string CREATE_USER = BASE_URL + "auth/register";
    public static readonly string SEND_CODE_REGISTER = BASE_URL + "auth/register/send-code";
    public static readonly string VERIFY_REGISTER = BASE_URL + "auth/register/verify";
    public static readonly string LOGIN = BASE_URL + "auth/login";
    //Login Register Confirm Password URLs

    //Brand Urls
    public static readonly string COMMON_BRANDS = BASE_URL + "common/brands";
    //Brand Urls

    //Products

    public static readonly string GET_ALL_PRODUCTS = BASE_URL + "common/products/view";
    public static readonly string GET_ALL_BY_ID = BASE_URL + "common/products/view";
    public static readonly string FILTER_BY_CATEGORY = BASE_URL + "common/products/filter?category=";
    //Products

    //User
    public static readonly string GET_USER_BY_ID = BASE_URL + "user/profile";
    //User
}
