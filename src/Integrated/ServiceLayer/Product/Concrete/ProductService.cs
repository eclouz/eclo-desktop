using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product.Concrete;

public class ProductService : IProductService
{
    public async Task<List<ProductViewModels>> GetAllProducts(int page)
    {
        using (var client = new HttpClient())
        {            
            var request = new HttpRequestMessage(HttpMethod.Get, API.GET_ALL_PRODUCTS+$"?page={page}");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
                        
            var result = await client.GetAsync(client.BaseAddress);
            string response = await result.Content.ReadAsStringAsync();
            IEnumerable<ProductViewModels> products = JsonConvert.DeserializeObject<List<ProductViewModels>>(response);
            
            List<ProductViewModels> productViewModelsList = new List<ProductViewModels>();                                    
            foreach (var i in products)
            {
                productViewModelsList.Add(new ProductViewModels()
                {
                    Id = i.Id,
                    ProductName=i.ProductName,
                    BrandName=i.BrandName,
                    ProductImagePath=i.ProductImagePath,
                    ProductColor=i.ProductColor,
                    ProductPrice=i.ProductPrice,
                    ProductDiscount=i.ProductDiscount,
                    ProductDescription=i.ProductDescription,
                    ProductSize=i.ProductSize,
                    ProductLiked=i.ProductLiked,
                });
            }
            return productViewModelsList;
        }
    }
}
