using Eclo.Domain.Entities;
using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModels.Products;

public class ProductViewModels : Auditable
{
    public long Id { get; set; }

    public string ProductName { get; set; } = String.Empty;

    public long BrandId { get; set; }

    public List<Brand> Brand { get; set; }
        = new List<Brand>();

    public List<ProductDetail> ProductDetail { get; set; }
        = new List<ProductDetail>();

    public double ProductPrice { get; set; }

    public List<float> ProductDiscount { get; set; }
        = new List<float>();

    public bool ProductLiked { get; set; } = false;
}
