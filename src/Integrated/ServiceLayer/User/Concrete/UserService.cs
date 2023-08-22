using Dtos.Auth;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text;

namespace Integrated.ServiceLayer.User.Concrete;

public class UserService : IUserService
{
    public async Task<bool> CreateUser(RegisterDto registerDto)
    {
        //using (var client = new HttpClient())
        //{

        //    client.BaseAddress = new Uri(API.CREATE_USER);

        //    string json = JsonConvert.SerializeObject(registerDto);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var result = await client.PostAsync(client.BaseAddress, content);
        //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //    {

        //        string response = await result.Content.ReadAsStringAsync();
        //        var res = JsonConvert.DeserializeObject<RegisterCheckDto>(response);

        //        return res.Result == true;

        //    }
        //    return false;
        //}


        var client = new HttpClient();
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

    public async Task<bool> SendCodeRegister(string phone)
    {
        using(var client = new HttpClient())
        {
            //MultipartFormDataContent    

            //client.BaseAddress = new Uri(API.SEND_CODE_REGISTER);

            //var requestData = new { phone };

            var phoneNumber = Uri.EscapeDataString(phone);
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://eclo.uz:8080/api/auth/register/send-code?phone=%2B{phone.Substring(1)}");
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

            

            //client.BaseAddress = new Uri(API.SEND_CODE_REGISTER);

            //string json = JsonConvert.SerializeObject(phone);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            //var result = await client.PostAsync(client.BaseAddress , content);
            //if(result.StatusCode==System.Net.HttpStatusCode.OK )
            //{
            //    return true;
            //}
            //return false;
        }
    }
}
