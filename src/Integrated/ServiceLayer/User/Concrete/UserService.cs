using Dtos.Auth;
using Eclo.DataAccess.ViewModels.Users;
using Newtonsoft.Json;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;

namespace Integrated.ServiceLayer.User.Concrete;

public class UserService : IUserService
{
    public async Task<bool> CreateUser(RegisterDto registerDto)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Post, API.BASE_URL + "auth/register");//API.CREATE_USER
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(registerDto.FirstName), "FirstName");
            content.Add(new StringContent(registerDto.LastName), "LastName");
            content.Add(new StringContent(registerDto.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(registerDto.Password), "Password");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string response2 = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<RegisterCheckDto>(response2);                
                return res.Result == true;
            }
            return false;
        }
    }

    public async Task<UserViewModel> GetUserById(long id, string token)
    {
        try
        {
            var client = new HttpClient();

            //Create request
            var request = new HttpRequestMessage(HttpMethod.Get, API.GET_USER_BY_ID + $"/{id}");

            //Add head Autharation token
            request.Headers.Add("Authorization", $"Bearer {token}");

            //Create content
            var content = new StringContent("", null, "text/plain");

            //Add content in request
            request.Content = content;

            //Send request
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserViewModel>(jsonString);
                return result;
            }
        }
        catch
        {

            return new UserViewModel();
        }


        return new UserViewModel();
    }

    public async Task<UserViewModel> GetUserByPhoneNumber(string phone)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get,
            API.BASE_URL + $"admin/users/userPhoneNumber?userPhoneNumber=%2B{(phone).Substring(1)}");
        var content = new StringContent("", null, "text/plain");
        request.Content = content;
        var response = await client.SendAsync(request);
        if(response.IsSuccessStatusCode)
        {
            var jsonstring = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserViewModel>(jsonstring);
            return result;
        }
        return new UserViewModel();
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Post, API.BASE_URL + "auth/login");
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //var token = IdentitySingleton.GetInstance();

                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent)!;
                //token.Token = jsonResponse.token.ToString();

                return true;
            }
            return false;
        }
    }

    public async Task<bool> SendCodeRegister(string phone)
    {
        using(var client = new HttpClient())
        {
            
            var request = new HttpRequestMessage(HttpMethod.Post, API.BASE_URL + "auth/register/send-code" + $"?phone=%2B{phone.Substring(1)}");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);

        
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;/////
                        
        }
    }

    public async Task<bool> UserUpdateSettings(UserViewModel dto)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, API.BASE_URL + $"user/profile/phoneNumber?phoneNumber=%2B{(dto.PhoneNumber).Substring(1)}");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent($"{dto.FirstName}"), "FirstName");
        content.Add(new StringContent($"{dto.LastName}"), "LastName");
        content.Add(new StreamContent(File.OpenRead($"{dto.ImagePath}")), "ImagePath", $"{dto.ImagePath}");
        content.Add(new StringContent($"{dto.PhoneNumber}"), "PhoneNumber");
        content.Add(new StringContent($"{dto.PassportSerialNumber}"), "PassportSerialNumber");
        content.Add(new StringContent($"{dto.BirthDate}"), "BirthDate");
        content.Add(new StringContent($"{dto.Region}"), "Region");
        content.Add(new StringContent($"{dto.District}"), "District");
        content.Add(new StringContent($"{dto.Address}"), "Address");
        request.Content = content;
        var response = await client.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsStringAsync();
            return true;
        }
        var res1 = await response.Content.ReadAsStringAsync();
        return false;
    }

    public async Task<bool> VerifyRegister(VerifyRegisterDto verifyRegisterDto)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Post,
                API.BASE_URL + "auth/register/verify" + $"?phoneNumber=%2B" +//API.VERIFY_REGISTER
                $"{(verifyRegisterDto.PhoneNumber).Substring(1)}&code={verifyRegisterDto.Code}");
            var content = new StringContent($"{{\r\n  \"phoneNumber\": \"{verifyRegisterDto.PhoneNumber}\"," +
                $"\r\n  \"code\": {verifyRegisterDto.Code}\r\n}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;////////////////////////////;
        }
    }
}
