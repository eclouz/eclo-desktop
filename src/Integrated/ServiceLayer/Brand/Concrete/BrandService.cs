using Newtonsoft.Json;
using ViewModels.Brands;

namespace Integrated.ServiceLayer.Brand.Concrete;

public class BrandService : IBrandService
{
    public async Task<IList<MensBrandsViewModels>> GetAllBrands(int page = 1)
    {

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(API.COMMON_BRANDS +
                $"?page={page}");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                IEnumerable<MensBrandsViewModels> readBrands = JsonConvert.DeserializeObject<IEnumerable<MensBrandsViewModels>>(responseData);
                List<MensBrandsViewModels> mensBrandsViewModels = new List<MensBrandsViewModels>();
                foreach (var i in readBrands)
                {
                    mensBrandsViewModels.Add(new MensBrandsViewModels()
                    {
                        Name = i.Name,
                        BrandIconPath = i.BrandIconPath,
                        CreatedAt = i.CreatedAt,
                        UpdatedAt = i.UpdatedAt,
                        Id = i.Id
                    });
                }
                return mensBrandsViewModels;
                // Process the itemList as needed
            }
            else
            {
                // Handle the response error accordingly
                // For example, throw an exception or return a default value
                return null;
            }

        }
    }
}

