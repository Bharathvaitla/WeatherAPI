using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;   
using System.Text;

namespace Weather_server.JwtHelper
{
    public class JwtHelper
    {
        public static string GenerateToken(string username)
        {   // 
            var key = new SymmetricSecurityKey(                                 
                Encoding.UTF8.GetBytes("averylongsecretkeyoflength256bits"));   // Converts string to byte[]

            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var claims = new[]  // Claim is info about user - Username, Email, Role, UserId
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                claims: claims, // Payload - user data that could be stored for authorization
                expires: DateTime.Now.AddMinutes(30),   // Expiry
                signingCredentials: creds   // Signature info
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Create & return the JWT
        }
    }
}
