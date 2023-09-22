using ViewModels.Brands;

namespace Integrated.ServiceLayer.Brand;

public interface IBrandService
{
    Task<IList<MensBrandsViewModels>> GetAllBrands(int page);

}
