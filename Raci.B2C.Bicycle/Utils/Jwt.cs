using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace Raci.B2C.Bicycle.Utils
{
    public static class Jwt
    {
        public static Dictionary<string, List<string>> CreateAuthorizationHeader(long? policyId)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            result["Authorization"] = new List<string> { "Bearer " + Jwt.CreateToken(policyId) };

            return result;
        }

        public static string CreateToken(long? policyId)
        {
            string applicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
            byte[] key = Convert.FromBase64String(applicationKey);

            SigningCredentials credentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(key),
                "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                "http://www.w3.org/2001/04/xmlenc#sha256");

            ClaimsIdentity subject = new ClaimsIdentity();

            if (policyId != null)
            {
                subject.AddClaim(new Claim("PolicyId", policyId.ToString()));
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                TokenIssuerName = "Satalyst",
                AppliesToAddress = "http://www.rac.com.au",
                SigningCredentials = credentials,
                Subject = subject,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}