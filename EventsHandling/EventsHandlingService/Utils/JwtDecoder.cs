using System.IdentityModel.Tokens.Jwt;

namespace ProjectAuthenticationAPI.Utils
{
    public class JwtDecoder
    {
        public static JwtSecurityToken DecodeToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);
            if(decodedToken == null)
            {
                throw new Exception("Invalid Jwt Token");
            }
            return decodedToken;
        }
        public static string GetClaimValue(JwtSecurityToken decodedToken, string claimType)
        {
            var claim = decodedToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
    }
}
