using Abstractions;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.Autenticacion
{
    public interface ITokenHandler
    {
        string GenerateTokenJWT(ITokenParameter parameter);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly JwtConfig _jwtConfig;
        public TokenHandler(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public string GenerateTokenJWT(ITokenParameter parameter)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, parameter.Id.ToString()),
                    new Claim(ClaimTypes.Name, parameter.Usuario),
                    new Claim(JwtRegisteredClaimNames.Email, parameter.Email),
                    new Claim("Password", parameter.Password),
                    new Claim(ClaimTypes.Role, parameter.Rol),
                    new Claim("CID", parameter.CID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
