using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProyectoPAU.Models;
using ProyectoPAU.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPAU.Services
{
    public class AutorizacionService : IAutorizacionService

    {
        private readonly TiendauContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(TiendauContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private String GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);


            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));


            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }


        private string GenerarRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";


            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);


            }

            return refreshToken;
        }


        



        private async Task<AutorizacionResponse> GuardarHistorialRefreshToken(
            int IdUsuario,
            string token,
            string refreshToken)
        {
            var historialRefreshToken = new HistorialRefreshToken
            {
                IdUsuario = IdUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddMinutes(2)
            };

           

            await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _context.SaveChangesAsync();


            return new AutorizacionResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };
            
        }

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
            x.Nombre == autorizacion.NombreUsuario && x.Contraseña == autorizacion.Clave);

            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);

            }

            string tokenCreado = GenerarToken(usuario_encontrado.UsuarioId.ToString());

            string refreshTokenCreado = GenerarRefreshToken();
            //return new AutorizacionResponse() { Token = tokenCreado , Resultado = true, Msg ="Ok" };
            return await GuardarHistorialRefreshToken(usuario_encontrado.UsuarioId, tokenCreado, refreshTokenCreado);

        }

        public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int IdUsuario)
        {
            var refreshTokenEncontrado = _context.HistorialRefreshTokens.FirstOrDefault(x => x.Token == refreshTokenRequest.TokeExpirado
            && x.RefreshToken == refreshTokenRequest.RefreshToken && x.IdUsuario == IdUsuario);

            if (refreshTokenEncontrado == null)
                return new AutorizacionResponse { Resultado = false, Msg = "NO EXISTE refresh TOKEN" };


            var refreshTokenCreado = GenerarRefreshToken();
            var tokenCreado = GenerarToken(IdUsuario.ToString());


            return await GuardarHistorialRefreshToken(IdUsuario, tokenCreado, refreshTokenCreado);


        }
    }
}
