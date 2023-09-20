
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;

namespace Integrated.ServiceLayer.Categories;

public interface ISubCategoryService
{
    Task<List<SubCategory>> GetAllSubCategories(int page);
}
