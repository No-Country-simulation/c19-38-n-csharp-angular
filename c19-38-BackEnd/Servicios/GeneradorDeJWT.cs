﻿using c19_38_BackEnd.Configuracion;
using c19_38_BackEnd.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace c19_38_BackEnd.Servicios
{
    public static class GeneradorDeJWT
    {
        public static IEnumerable<Claim> ObtenerClaims(Usuario usuario, string rol)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                new Claim(ClaimTypes.Name,usuario.Nombre),
                new Claim(ClaimTypes.Email,usuario.Email),
                new Claim(ClaimTypes.Role,rol)
            };
        }

        public static string GenerarJwt(Usuario usuario,string rol,JwtSettings jwtSettings)
        {
            byte[] keyEnBytes = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

            var jwtToken = new JwtSecurityToken(
                issuer:jwtSettings.ValidIssuer,
                audience:jwtSettings.ValidAudience,
                claims:ObtenerClaims(usuario,rol),
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(keyEnBytes),SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
                
        }
    }
}
