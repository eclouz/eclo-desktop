using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product;

public interface IProductService
{

    Task<(List<ProductViewModels> productViewModels, Pagination pageData)> GetAllProducts(long id ,int page);
    Task<ProductGetViewModel> GetByIdProducts(long userId , long id, string token);
    Task<List<ProductViewModels>> FilterBYCategories(long userId,string categoryString, int min, int max, List<string> subCategoriesName, int page);
    Task<bool> UserSetLikeTrue(long userId,long productId,bool isLiked=true);
    Task<bool> DeleteLike(long likeId, string token);
    Task<List<GetUserProductLikes>> getUserProductLikes(int page,string token);
    Task<bool> UserProductLikeUpdate(long likeId, long userId, long productId, bool isLiked,string token);
    
}
