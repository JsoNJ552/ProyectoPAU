using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.UsuariosService;
using ProyectoPAU.Services.VentasService;

namespace ProyectoPAU.Controllers
{
	public class AdminFacturasController : Controller
	{

        private readonly IVentasServicecs _ventasService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TiendauContext _tiendauContext;
        private readonly IUsuariosService _usuariosService;


        public AdminFacturasController(IVentasServicecs ventasServicecs, IHttpContextAccessor httpContextAccessor, IUsuariosService usuarioService, TiendauContext tiendauContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _ventasService = ventasServicecs;
            _tiendauContext = tiendauContext;
            _usuariosService = usuarioService;

        }
        public async Task<IActionResult> Index()
        {
            var ventas = await _ventasService.ObtenerTodasLasVentas();
            var ventasyDetalle =  _ventasService.ObtenerTodasDetalleVentasPorVenta(ventas);
           
            return View(ventasyDetalle);
        }

        public async  Task<IActionResult> VerDetalleFactura(int idVenta)
        {
            var precioFinalVenta = 0;




            var VentaDetallesPrimera = await _ventasService.ObtenerDetalleVentasPorIdVenta(idVenta);

            foreach (var precioTotal in VentaDetallesPrimera )
            {
                 precioFinalVenta = precioFinalVenta+(int)precioTotal.PrecioTotal;
                
            }


            ViewData["PrecioFinal"] = precioFinalVenta;




            return View(VentaDetallesPrimera);
        }
    }
}
