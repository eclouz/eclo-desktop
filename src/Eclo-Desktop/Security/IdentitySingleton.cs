using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclo_Desktop.Security;

public class IdentitySingleton
{
    private static IdentitySingleton _identitySingleton;
    //properties 
    public long UserId { get; set; }
    private IdentitySingleton()
    {
        _identitySingleton = new IdentitySingleton();
    }
    public static IdentitySingleton GetInstance()
    {
        if(_identitySingleton == null)
        {
            _identitySingleton = new IdentitySingleton();
        }
            return _identitySingleton;
    }

}
