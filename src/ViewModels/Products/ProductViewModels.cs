using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Products;

public class ProductViewModels
{
    public long Id { get; set; }

    public string ProductName { get; set; } = String.Empty;

    public string BrandName { get; set; } = String.Empty;

    public string ProductImagePath { get; set; } = String.Empty;

    public string ProductColor { get; set; } = String.Empty;

    public double ProductPrice { get; set; }

    public int ProductDiscount { get; set; }

    public string ProductDescription { get; set; } = String.Empty;

    public string ProductSize { get; set; } = String.Empty;

    public bool ProductLiked { get; set; } = false;
}
