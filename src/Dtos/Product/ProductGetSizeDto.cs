using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Product;


public class ProductGetSizeDto
{
    public long Id { get; set; }
    public string Size { get; set; }
    public int quantity { get; set; }

}
