using API_DACN.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class Token
    {
        private IConfiguration _configuration;
        private readonly food_location_dbContext db;

        public Token(IConfiguration config, food_location_dbContext context)
        {
            _configuration = config;
            db = context;
        }

        public string GetToken(string user)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim("Group", "PSTeam"),
                    new Claim("Email", "covid21tsp@gmail.com"),
                    new Claim("Hosting", "covid21tsp.space"),
                    new Claim("UserID", user)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetPhoneWithToken(IHeaderDictionary Headers)
        {
            Dictionary<string, string> requestHeaders =
               new Dictionary<string, string>();
            foreach (var header in Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }

            var stream = requestHeaders["Authorization"].Substring(7);

            var token = new JwtSecurityToken(jwtEncodedString: stream);
            string t = token.Claims.First(c => c.Type == "UserID").Value;

            return t;
        }
    }
}
