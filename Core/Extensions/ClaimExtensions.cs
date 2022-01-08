using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim("name", name));
        }
        public static void AddDepoName(this ICollection<Claim> claims, string depo)
        {
            claims.Add(new Claim("depo", depo));
        }

        public static void AddFkDepo(this ICollection<Claim> claims, string fkDepo)
        {
            claims.Add(new Claim("fkDepo", fkDepo));
        }

        public static void AddDepoTip(this ICollection<Claim> claims, string depoTip)
        {
            claims.Add(new Claim("depoTip", depoTip));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim("nameIdentifier", nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim("role", role)));
        }
    }
}
