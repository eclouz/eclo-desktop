using System.IdentityModel.Tokens.Jwt;

namespace Eclo_Desktop.Utilities;

public static class DecodeJwtToken
{
    public static (bool success, JwtSecurityToken token) DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Tokenni dekod qilish
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return (true, securityToken)!;
        }
        catch
        {
            // Token dekod qilinishida xatolik yuzaga keldi
            // Xatoni qaytarish va tekshirishni davom ettirish mumkin
            return (false!, null!);
        }
    }
}
