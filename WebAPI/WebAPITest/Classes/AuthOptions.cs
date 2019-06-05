using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Classes
{
    public class AuthOptions
    {
        public string Issuer { get; } = "WebApi_Issuer"; // издатель токена (любое название)
        public string Audience { get; } = "WebApi_Audience"; // потребитель токена (любое название)
        public string Key { get; set; } // ключ для шифрования
        public int Lifetime { get; }  = 60; // время жизни токена - 1 час
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
    }
}
