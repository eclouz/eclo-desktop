using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Brands;

namespace Integrated.ServiceLayer.Brand;

public  interface IBrandService
{
    Task<IList<MensBrandsViewModels>> GetAllBrands(int page);

}
