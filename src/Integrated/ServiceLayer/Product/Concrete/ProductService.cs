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
using ViewModels;
using ViewModels.Brands;
using ViewModels.Common;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product.Concrete;

public class ProductService : IProductService
{
    public async Task<bool> DeleteLike(long likeId, string token)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Delete, API.BASE_URL + $"user/product/likes/{likeId}");
        //Add head Autharation token
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Headers.Add("phone", "+998902723595");
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("phone", "+998902723595"));
        var content = new FormUrlEncodedContent(collection);
        request.Content = content;
        var response = await client.SendAsync(request);
        if(response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<List<ProductViewModels>> FilterBYCategories(long userId, string categoryString, int min, int max, List<string> subCategoriesName, int page)
    {
        string subCategoriesString = "";
        for (int i = 0; i < subCategoriesName.Count; i++)
        {
            subCategoriesString += "subCategoriesName=";
            subCategoriesString += subCategoriesName[i];
            subCategoriesString += "&";
        }
        var client = new HttpClient();
        if (subCategoriesString.Length > 0)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, API.BASE_URL + $"common/products/filter/category?userId={userId}&categoryName={categoryString}&min={min}&max={max}&{subCategoriesString}page={page}");
            var content = new StringContent("", null, "text/plain");
            var response = await client.SendAsync(request);
            request.Content = content;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductViewModels>>(jsonString);
                List<ProductViewModels> productViewModelsList = new List<ProductViewModels>();
                foreach (var i in result)
                {
                    productViewModelsList.Add(new ProductViewModels()
                    {
                        Id = i.Id,
                        ProductName = i.ProductName,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
                        ProductDetail = i.ProductDetail,
                        ProductPrice = i.ProductPrice,
                        ProductDiscount = i.ProductDiscount,
                        ProductLiked = i.ProductLiked,
                        likedId = i.likedId,
                        SubCategory = i.SubCategory

                    });
                }
                return productViewModelsList;
            }
            else
            {
                return new List<ProductViewModels>();
            }
        }
        else
        {
            var request = new HttpRequestMessage(HttpMethod.Get, API.BASE_URL + $"common/products/filter/category?userId={userId}&categoryName={categoryString}&min={min}&max={max}&page={page}");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductViewModels>>(jsonString);
                List<ProductViewModels> productViewModelsList = new List<ProductViewModels>();
                foreach (var i in result)
                {
                    List<float> list = new List<float>();
                    foreach (var j in i.ProductDiscount)
                    {
                        list.Add(j);
                    }
                    productViewModelsList.Add(new ProductViewModels()
                    {
                        Id = i.Id,
                        ProductName = i.ProductName,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
                        ProductDetail = i.ProductDetail,
                        ProductPrice = i.ProductPrice,
                        ProductDiscount = i.ProductDiscount,
                        ProductLiked = i.ProductLiked,
                        likedId = i.likedId,
                        SubCategory = i.SubCategory
                    });
                }
                return productViewModelsList;
            }
            else
            {
                return new List<ProductViewModels>();
            }
        }

    }

    public async Task<(List<ProductViewModels> productViewModels,Pagination pageData)> GetAllProducts(long id,int page = 1)
    {
        using (var client = new HttpClient())
        {
            Pagination pagination = new Pagination();
            var response = await client.GetAsync(API.BASE_URL + $"common/products/view/user?userId={id}&page={page}");
            response.EnsureSuccessStatusCode();
         

            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("x-pagination", out var headerValues))
                {
                    string headerValue = headerValues.FirstOrDefault();
                    pagination = Newtonsoft.Json.JsonConvert.DeserializeObject<Pagination>(headerValue);
                    
                }
               
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
                        likedId = i.likedId,
                        SubCategory = i.SubCategory

                    }) ;
                }
                return (productViewModels:productList,pageData:pagination);
            }
            else
            {
                return (new List<ProductViewModels>(),new Pagination());
            }
        }

    }

    public async Task<ProductGetViewModel> GetByIdProducts(long userId,long id, string token)
    {
        using (var client = new HttpClient())
        {
            
            var request = new Uri(API.BASE_URL + $"common/products/view/user/{id}?userId={userId}");

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
                i.likedId = readProducts.likedId;
                i.ProductLiked = readProducts.ProductLiked;
                i.ProductComments = readProducts.ProductComments;
                i.Id = readProducts.Id;
                return i;
            }
            else
            {
                return new ProductGetViewModel();
            }
        }
    }

    public async Task<List<GetUserProductLikes>> getUserProductLikes(int page, string token)
    {
        
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, API.BASE_URL + $"common/user/product/likes?page={page}");
        //Add head Autharation token
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(request);
        if(response.IsSuccessStatusCode) 
        {
            string jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GetUserProductLikes>>(jsonString);
            return result;
        }
        return new List<GetUserProductLikes>();

    }

    public async Task<bool> UserProductLikeUpdate(long likeId, long userId, long productId, bool isLiked, string token)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, API.BASE_URL + $"user/product/likes/{likeId}");
        //Add head Autharation token
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Headers.Add("UserId", $"{userId}");
        request.Headers.Add("ProductId", $"{productId}");
        request.Headers.Add("IsLiked", $"{isLiked}");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent($"{userId}"), "UserId");
        content.Add(new StringContent($"{productId}"), "ProductId");
        content.Add(new StringContent($"{isLiked}"), "IsLiked");
        request.Content = content;
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {            
           string json = await response.Content.ReadAsStringAsync();
            if (json == "false")
            {
                return false;
            } 
            return true;
            
        }
        else
        { return false; }

    }

    public async Task<bool> UserSetLikeTrue(long userId, long productId,string token, bool isLiked = true)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, API.BASE_URL + "user/product/likes");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent($"{userId}"), "UserId");
        content.Add(new StringContent($"{productId}"), "ProductId");
        content.Add(new StringContent($"{true}"), "IsLiked");
        request.Content = content;
        var response = await client.SendAsync(request);
        if(response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }
}
