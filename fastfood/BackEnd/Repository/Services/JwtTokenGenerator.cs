﻿using BackEnd.Models;
using BackEnd.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Repository.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(User user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
            {
                new Claim( JwtRegisteredClaimNames.Email, user.Email ),
                new Claim( JwtRegisteredClaimNames.Sub, user.Id),
                new Claim( JwtRegisteredClaimNames.Name, user.Name),
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
