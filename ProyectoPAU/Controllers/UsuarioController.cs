using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProyectoPAU.Models.Custom;
using ProyectoPAU.Models.DTO;
using ProyectoPAU.Services.Auth;
using ProyectoPAU.Services.LoginService;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ProyectoPAU.Models;

namespace ProyectoPAU.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;
        private readonly ILoginService _loginService;
        private readonly TiendauContext _tiendauContext;

        public UsuarioController(IAutorizacionService autorizacionService, ILoginService loginService, TiendauContext tiendauContext)
        {
            _autorizacionService = autorizacionService;
            _loginService = loginService;
            _tiendauContext = tiendauContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser usuario)
        {
            try
            {
                var usuarioValido = await _loginService.ValidateUserAsync(usuario);

                if (usuarioValido!=null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, usuarioValido.Nombre),
                    new Claim("Correo", usuario.email)
                    


                    };
                    var rol = await _tiendauContext.Rols.FindAsync(usuarioValido.RolId);
                    if (rol != null)
                    {
                        string rolnombre = rol.Nombre;
                        claims.Add(new Claim(ClaimTypes.Role, rolnombre));
                    }


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                  
                };
              

                // Credenciales correctas
                return Ok("Credenciales correctas");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, "" + ex.Message);
            }
        }


        public async Task<IActionResult> mostrarUsuarios()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }




        public async Task <IActionResult> CerrarSesion()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");

        }



        [HttpPost]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
            if (resultado_autorizacion == null)
                return Unauthorized();
            return Ok(resultado_autorizacion);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(request.TokeExpirado);

            if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
            {
                return BadRequest(new AutorizacionResponse { Resultado = false, Msg = "Token no ha expirado" });
            }

            string idUsuario = tokenExpiradoSupuestamente.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

            var autorizacionResponse = await _autorizacionService.DevolverRefreshToken(request, int.Parse(idUsuario));

            if (autorizacionResponse.Resultado)
            {
                return Ok(autorizacionResponse);
            }
            else
            {
                return BadRequest(autorizacionResponse);
            }
        }
    }
}
