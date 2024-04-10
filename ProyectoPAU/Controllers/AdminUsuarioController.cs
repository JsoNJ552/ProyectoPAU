using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.UsuariosService;
using ProyectoPAU.Services.VentasService;

namespace ProyectoPAU.Controllers
{
    public class AdminUsuarioController : Controller
    {
        private readonly IVentasServicecs _ventasService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TiendauContext _tiendauContext;
        private readonly IUsuariosService _usuariosService;


        public AdminUsuarioController(IVentasServicecs ventasServicecs, IHttpContextAccessor httpContextAccessor, IUsuariosService usuarioService, TiendauContext tiendauContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _ventasService = ventasServicecs;
            _tiendauContext = tiendauContext;
            _usuariosService = usuarioService;

        }


        public IActionResult Index()
        {

            try
            {
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
    }
}
