using Dtos.Auth;
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
            var request = new HttpRequestMessage(HttpMethod.Post, API.CREATE_USER);
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

    public async Task<bool> Login(LoginDto loginDto)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Post, API.LOGIN);            
            var content = new StringContent($"{{\r\n  \"phoneNumber\": \"{loginDto.PhoneNumber}\"," +
                $"\r\n  \"code\": {loginDto.Password}\r\n}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }

    public async Task<bool> SendCodeRegister(string phone)
    {
        using(var client = new HttpClient())
        {            
                       
            var phoneNumber = Uri.EscapeDataString(phone);
            var request = new HttpRequestMessage(HttpMethod.Post, API.SEND_CODE_REGISTER+
                $"?phone=%2B{phone.Substring(1)}");
            request.Headers.Add("phone", phone);
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("phone", phone));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
                        
        }
    }

    public async Task<bool> VerifyRegister(VerifyRegisterDto verifyRegisterDto)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Post,
                API.VERIFY_REGISTER+$"?phoneNumber=%2B" +
                $"{(verifyRegisterDto.PhoneNumber).Substring(1)}&code={verifyRegisterDto.Code}");
            var content = new StringContent($"{{\r\n  \"phoneNumber\": \"{verifyRegisterDto.PhoneNumber}\"," +
                $"\r\n  \"code\": {verifyRegisterDto.Code}\r\n}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
