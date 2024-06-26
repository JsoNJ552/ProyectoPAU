﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoPAU.Models;
using ProyectoPAU.Services.UsuariosService;
using ProyectoPAU.Services.VentasService;
using System.Net.Http;

namespace ProyectoPAU.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IVentasServicecs _ventasService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TiendauContext _tiendauContext;
        private readonly IUsuariosService _usuariosService;

        public PerfilController(IVentasServicecs ventasServicecs, IHttpContextAccessor httpContextAccessor,IUsuariosService usuarioService, TiendauContext tiendauContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _ventasService = ventasServicecs;
            _tiendauContext = tiendauContext;
            _usuariosService = usuarioService;

        }


        public async Task<IActionResult> Index()
        {

            try
            {
                var carritoDetalle = HttpContext.Items["Carrito"] as List<CarritoDetalle>;
                ViewData["Carrito"] = carritoDetalle;

                int cantidadProductosCarro = 0;
                foreach (var cantidadProductosCarrito in carritoDetalle as List<CarritoDetalle>)
                {
                    cantidadProductosCarro = cantidadProductosCarro + (int)cantidadProductosCarrito.Cantidad;

                }

                ViewData["CantidadProductos"] = cantidadProductosCarro;


                var idUsuario = HttpContext.Session.GetInt32("idUsuario");
                var email = HttpContext.Session.GetString("Email");
                var apellido = HttpContext.Session.GetString("Apellido");
                var nombre = HttpContext.Session.GetString("Nombre");

                // Crear un modelo con los datos de la sesión
                ViewData["IdUsuario"] = idUsuario;
                ViewData["Email"] = email;
                ViewData["Apellido"] = apellido;
                ViewData["Nombre"] = nombre;




                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                var facturas = _ventasService.ObtenerVentasPorID((int)usuarioIDNullable);
                var VentaDetallesPrimera = _ventasService.ObtenerDetalleVentasPorUsuarioId(facturas);
            



                return View(VentaDetallesPrimera);

            }
            catch (Exception ex)
            {

            }



            return View();
        }

        public async Task<IActionResult> FacturaDetalle(int Venta)
        {

            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                var facturas = _ventasService.ObtenerVentasPorID((int)usuarioIDNullable);
                var VentaDetallesPrimera = await _ventasService.ObtenerDetalleVentasPorIdVenta(Venta);

                var precioFinalVenta = 0;

                foreach (var precioTotal in VentaDetallesPrimera)
                {
                    precioFinalVenta = precioFinalVenta + (int)precioTotal.PrecioTotal;

                }

                ViewData["PrecioFinal"] = precioFinalVenta;

                return View(VentaDetallesPrimera);

            }
            catch (Exception ex)
            {

            }



            return View();
        }





        public async Task<IActionResult>editarPerfil()
        {

            try
            {
                var idUsuario = HttpContext.Session.GetInt32("idUsuario");
                var email = HttpContext.Session.GetString("Email");
                var apellido = HttpContext.Session.GetString("Apellido");
                var nombre = HttpContext.Session.GetString("Nombre");

                // Crear un modelo con los datos de la sesión
                ViewData["IdUsuario"] = idUsuario;
                ViewData["Email"] = email;
                ViewData["Apellido"] = apellido;
                ViewData["Nombre"] = nombre;



            }
            catch (Exception ex)
            {

            }



            return View();
        }


        [HttpPost]
        public async Task<IActionResult> guardarEditPerfil([FromBody] Usuario usuario)
        {

            
            try
            {
               
                usuario.Email= HttpContext.Session.GetString("Email");
                var UsuarioEditar = await _usuariosService.obtenerUsuarioPorEmail(usuario);
                UsuarioEditar.Nombre = usuario.Nombre;
                UsuarioEditar.Email = usuario.Email;
                await _usuariosService.EditarUsuario(UsuarioEditar);

                return Ok("Ok");

            }
            catch (Exception ex) { 
            
            }



            return View();

        }


        public async Task<IActionResult> VerificarContraseña()
        {

            var email = HttpContext.Session.GetString("Email");
            ViewData["email"] = email;



            return View();

        }



        public async Task<IActionResult> VerificarContraseñaEmail()
        {

            var email = HttpContext.Session.GetString("Email");
            ViewData["email"] = email;



            return View();

        }

        public async Task<IActionResult> ViewCambiarEmail()
        {

            var email = HttpContext.Session.GetString("Email");
            ViewData["email"] = email;



            return View();

        }

        public async Task<IActionResult> ViewCambiarPassword()
        {


            var httpContext = _httpContextAccessor.HttpContext;
            int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");




            return View();

        }




        [HttpPost]
        public async Task<IActionResult> CambiarEmail([FromBody] Usuario email)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                // Obtener el usuario actual por su ID
                Usuario usuario = await _usuariosService.obtenerUsuarioPorId((int)usuarioIDNullable);

                // Verificar si el nuevo email ya está en uso
                var usuarioEmail = await _usuariosService.obtenerUsuarioPorEmail(email);
                if (usuarioEmail != null)
                {
                    return BadRequest("El Email ya existe");
                }

                // Actualizar el email del usuario actual
                usuario.Email = email.Email;
                await _usuariosService.EditarUsuario(usuario);

                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest("bad");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CambiarPassword([FromBody]Usuario password)
        {

            try
            {

                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                Usuario usuario = await _usuariosService.obtenerUsuarioPorId((int)usuarioIDNullable);
                Console.Write("CONTRAA"+" "+usuario.Contraseña);
                string HashPassword = BCrypt.Net.BCrypt.HashPassword(password.Contraseña);
                usuario.Contraseña = HashPassword;

                await _usuariosService.EditarUsuario(usuario);

                return Ok("Ok");
            }
            catch (Exception ex)
            {

                return BadRequest("bad");

            }


        }


    }
}
