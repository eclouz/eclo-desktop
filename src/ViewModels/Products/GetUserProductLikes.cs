using Eclo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Products;

public class GetUserProductLikes : Auditable
{
    public long userId {  get; set; }
    public long productId { get; set; }
    public bool isLiked { get; set; }   

}
