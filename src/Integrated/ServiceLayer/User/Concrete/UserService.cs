using Dtos.Auth;
using Newtonsoft.Json;
using System.Text;

namespace Integrated.ServiceLayer.User.Concrete;

public class UserService : IUserService
{
    public async Task<bool> CreateUser(RegisterDto registerDto)
    {
        using (var client = new HttpClient())
        {

            client.BaseAddress = new Uri(API.CREATE_USER);

            string json = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(client.BaseAddress, content);
            if(result.StatusCode==System.Net.HttpStatusCode.Created)
            {
            var response = await result.Content.ReadAsStringAsync();
                return response == "true";

            }
            return false;
        }
    }
}
