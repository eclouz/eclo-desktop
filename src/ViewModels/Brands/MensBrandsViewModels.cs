using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Brands;

public class MensBrandsViewModels
{    
    public string Name { get; set; } = String.Empty;
    public string BrandIconPath { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long Id { get; set; }

}
