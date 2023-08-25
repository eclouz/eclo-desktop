using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
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
    public async Task<List<ProductViewModels>> GetAllProducts(int page = 1)
    {

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(API.GET_ALL_PRODUCTS + $"/view?page={page}");
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
                        ProductName = i.ProductName,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
                        ProductDetail = i.ProductDetail,
                        ProductPrice = i.ProductPrice,
                        ProductDiscount = i.ProductDiscount,
                        ProductLiked = i.ProductLiked,
                    });
                }
                return productList;
            }
            else
            {
                return new List<ProductViewModels>();
            }
        }

    }

    public async Task<ProductGetViewModel> GetByIdProducts(long id)
    {
        using (var client = new HttpClient())
        {
            
            var request = new Uri(API.GET_ALL_BY_ID + $"/{id}");
            HttpResponseMessage response1;                                    
            response1 = await client.GetAsync(request);            
            if (response1.IsSuccessStatusCode)
            {
                var responseData = await response1.Content.ReadAsStringAsync();
                var readProducts = JsonConvert.DeserializeObject<ProductGetViewModel>(responseData);
                ProductGetViewModel i = new ProductGetViewModel();
                i.ProductName = readProducts.ProductName;
                i.BrandId = readProducts.BrandId;
                i.Brand = readProducts.Brand;
                i.ProductDetail = readProducts.ProductDetail;
                i.ProductPrice = readProducts.ProductPrice;
                i.ProductDescription = readProducts.ProductDescription;
                i.SubCategoryId = readProducts.SubCategoryId;
                i.SubCategory = readProducts.SubCategory;
                i.ProductDiscount = readProducts.ProductDiscount;
                i.ProductLiked = readProducts.ProductLiked;
                i.ProductComments = readProducts.ProductComments;
                
                return i;
            }
            else
            {
                return new ProductGetViewModel();
            }
        }
    }
}
