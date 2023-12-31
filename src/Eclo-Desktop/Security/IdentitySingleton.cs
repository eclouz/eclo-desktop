using System.Collections.Generic;
using ViewModels.Common;
using ViewModels.ShoppingCharts;

namespace Eclo_Desktop.Security;

public class IdentitySingleton
{
    private static IdentitySingleton _identitySingleton;
    //properties 

    public long UserId { get; set; }
    public double TotalPrice { get; set; }
    public string Token { get; set; }
    public IList<ShoppingChartViewModel> ShoppingChartProducts { get; set; } = new List<ShoppingChartViewModel>();
    public Pagination pagination { get; set; }
    private IdentitySingleton()
    {

    }
    public static IdentitySingleton GetInstance()
    {
        if (_identitySingleton == null)
        {
            _identitySingleton = new IdentitySingleton();
        }
        return _identitySingleton;
    }

}
