
using Eclo.Domain.Entities.Categories;

namespace Integrated.ServiceLayer.Categories;

public interface ISubCategoryService
{
    Task<List<SubCategory>> GetAllSubCategories(int page);
}
