using Eclo.Domain.Entities.Discounts;

namespace Integrated.ServiceLayer.IDiscountService;

public interface IDiscountService
{
    Task<List<Discount>> GetAllDiscounts(int page);
}
