using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Brands;

namespace Integrated.ServiceLayer.Brand.Concrete;

public class BrandService : IBrandService
{
    public async Task<IList<MensBrandsViewModels>> GetAllBrands(int page=1)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Get, API.COMMON_BRANDS+
                $"?page={page}");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;

            var res = await client.GetAsync(client.BaseAddress);

            string rsp = await res.Content.ReadAsStringAsync();

            var response = await client.SendAsync(request);

            IEnumerable<MensBrandsViewModels> readBrands = JsonConvert.DeserializeObject<IEnumerable<MensBrandsViewModels>>(rsp);

            List<MensBrandsViewModels> mensBrandsViewModels = new List<MensBrandsViewModels>();

            foreach(var i in readBrands)
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
        }
    }
}
