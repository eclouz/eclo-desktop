using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product;

public interface IProductService
{
    Task<List<ProductViewModels>> GetAllProducts(long id ,int page);
    Task<ProductGetViewModel> GetByIdProducts(long userId , long id);
    Task<List<ProductViewModels>> FilterBYCategories(long userId,string categoryString,int pgae);
    Task<bool> UserSetLikeTrue(long userId,long productId,bool isLiked=true);
    Task<bool> DeleteLike(long likeId);
    Task<List<GetUserProductLikes>> getUserProductLikes(int page);
    Task<bool> UserProductLikeUpdate(long likeId, long userId, long productId, bool isLiked);
    
}
