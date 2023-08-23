using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Brands;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product.Concrete;

public class ProductService : IProductService
{
    public async Task<List<ProductViewModels>> GetAllProducts(int page=1)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(API.GET_ALL_PRODUCTS + $"?page={page}");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                IEnumerable<ProductViewModels> readProducts = JsonConvert.DeserializeObject<IEnumerable<ProductViewModels>>(responseData);
                List<ProductViewModels> productList = new List<ProductViewModels>();
                foreach (var i in readProducts)
                {
                    productList.Add(new ProductViewModels()
                    {
                        Id = i.Id,
                        ProductName=i.ProductName,
                        BrandId=i.BrandId,
                        Brand=i.Brand,
                        ProductDetail= i.ProductDetail,
                        ProductPrice =i.ProductPrice,
                        ProductDiscount =i.ProductDiscount,
                        ProductLiked =i.ProductLiked,
                    });
                }
                return productList;
                // Process the itemList as needed
            }
            else
            {
                // Handle the response error accordingly
                // For example, throw an exception or return a default value
                return new List<ProductViewModels>();
            }
        }
}
