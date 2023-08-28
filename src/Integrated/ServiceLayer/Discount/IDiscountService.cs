using Eclo.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrated.ServiceLayer.IDiscountService;

public interface IDiscountService
{
    Task<List<Discount>> GetAllDiscounts(int page);
}
