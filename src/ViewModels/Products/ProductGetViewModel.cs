using Eclo.Domain.Entities;
using Eclo.Domain.Entities.Brands;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;
using System.Text.Json.Serialization;


namespace ViewModels.Products;

public class ProductGetViewModel : Auditable
{
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = String.Empty;
    [JsonPropertyName("brandId")]
    public long BrandId { get; set; }
    [JsonPropertyName("brand")]
    public List<Brand> Brand { get; set; }
        = new List<Brand>();
    [JsonPropertyName("productDetail")]
    public List<ProductDetail> ProductDetail { get; set; }
        = new List<ProductDetail>();
    [JsonPropertyName("productPrice")]
    public double ProductPrice { get; set; }
    [JsonPropertyName("productDescription")]
    public string ProductDescription { get; set; }
    [JsonPropertyName("subCategoryId")]
    public long SubCategoryId { get; set; }
    [JsonPropertyName("subCategory")]
    public List<SubCategory> SubCategory { get; set; }
        = new List<SubCategory>();
    [JsonPropertyName("productDiscount")]
    public List<ProductDiscount> ProductDiscount { get; set; }
        = new List<ProductDiscount>();
    [JsonPropertyName("productLiked")]
    public bool ProductLiked { get; set; } = false;
    [JsonPropertyName("productComments")]
    public List<ProductComment> ProductComments { get; set; }
      = new List<ProductComment>();
    //public string productName { get; set; } = String.Empty;

    //public long BrandId { get; set; }

    //public List<Brand> Brand { get; set; }
    //    = new List<Brand>();

    //public List<ProductDetail> ProductDetail { get; set; }
    //    = new List<ProductDetail>();

    //public double ProductPrice { get; set; }

    //public double ProductDescription { get; set; }

    //public long SubCategoryId { get; set; }

    //public List<SubCategory> SubCategory { get; set; }
    //    = new List<SubCategory>();

    //public List<ProductDiscount> ProductDiscount { get; set; }
    //    = new List<ProductDiscount>();

    //public bool ProductLiked { get; set; } = false;

    //public List<ProductComment> ProductComments { get; set; }
    //  = new List<ProductComment>();
}
