using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.VentasService;

namespace ProyectoPAU.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IVentasServicecs _ventasService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TiendauContext _tiendauContext;

        public PerfilController(IVentasServicecs ventasServicecs, IHttpContextAccessor httpContextAccessor, TiendauContext tiendauContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _ventasService = ventasServicecs;
            _tiendauContext = tiendauContext;
            

        }


        public async Task <IActionResult>Index()
        {

            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                var facturas =   _ventasService.ObtenerVentasPorID((int)usuarioIDNullable);
                var VentaDetallesPrimera = _ventasService.ObtenerDetalleVentasPorUsuarioId(facturas);

                foreach (var pr in facturas)
                {
                    Console.WriteLine(pr.IdVenta + "VentaID");
                }

                return View(VentaDetallesPrimera);

            }
            catch (Exception ex)
            {

            }
            


            return View();
        }

        public async Task<IActionResult>FacturaDetalle(int idVenta)
        {

            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                var facturas =  _ventasService.ObtenerVentasPorID((int)usuarioIDNullable);
                var VentaDetallesPrimera =await _ventasService.ObtenerVentas(idVenta);

                foreach (var pr in facturas)
                {
                    Console.WriteLine(pr.IdVenta + "VentaID");
                }

                return View(VentaDetallesPrimera);

            }
            catch (Exception ex)
            {

            }
           


            return View();
        }


    }
}
