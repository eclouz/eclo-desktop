using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Products;

namespace Integrated.ServiceLayer.Product;

public interface IProductService
{
    Task<List<ProductViewModels>> GetAllProducts(int page);
}
