using Eclo.Domain.Entities;

namespace ViewModels.Products;

public class GetUserProductLikes : Auditable
{
    public long userId { get; set; }
    public long productId { get; set; }
    public bool isLiked { get; set; }

}
