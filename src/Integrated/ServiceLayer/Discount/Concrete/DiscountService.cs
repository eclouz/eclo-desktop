using Eclo.Domain.Entities.Discounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer.IDiscountService.Concrete;

public class DiscountService : IDiscountService
{
    public async Task<List<Discount>> GetAllDiscounts(int page)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, API.BASE_URL+$"common/discounts?page={page}");
        request.Headers.Add("UserId", "");
        request.Headers.Add("ProductId", "");
        request.Headers.Add("IsLiked", "");
        var content = new StringContent("", null, "text/plain");
        request.Content = content;
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            List<Discount> discounts = new List<Discount>();
            string jsonstring = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Discount>>(jsonstring);
            foreach (var item in result)
            {
                discounts.Add(item);
            }
            return discounts;
        }
        else return new List<Discount>();

    }
}
