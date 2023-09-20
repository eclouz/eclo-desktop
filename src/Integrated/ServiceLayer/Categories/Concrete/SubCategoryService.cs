
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Newtonsoft.Json;

namespace Integrated.ServiceLayer.Categories.Concrete;

public class SubCategoryService : ISubCategoryService
{
    public async Task<List<SubCategory>> GetAllSubCategories(int page)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, API.BASE_URL + $"common/subcategories?page={page}");
        request.Headers.Add("CategoryId", "");
        request.Headers.Add("Id", "");
        request.Headers.Add("Name", "");
        var content = new StringContent("", null, "text/plain");
        request.Content = content;
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            List<SubCategory> subCategory = new List<SubCategory>();
            string jsonstring = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SubCategory>>(jsonstring);
            foreach (var item in result)
            {
                subCategory.Add(item);
            }
            return subCategory;
        }
        else return new List<SubCategory>();
    }
}
