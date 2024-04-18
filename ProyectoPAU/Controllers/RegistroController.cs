using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.Registro;

namespace ProyectoPAU.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendauContext _context;
        private readonly IRegistroUsuario _registro;
        private readonly IHttpContextAccessor _httpContextAccesso;

        public RegistroController (TiendauContext context, IRegistroUsuario registro, IHttpContextAccessor httpContextAccesso)
        {
            _context = context;
            _registro = registro;
            _httpContextAccesso = httpContextAccesso;
         
            

        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public  async Task <IActionResult> Registrar(Usuario model)
        {


            try
            {
                


                if (_registro.IsRegistrado(model.Email))
                {
                    return View("Index");
                }

                model.RolId = 2;
                model.Activo = true;
                await _registro.RegistrarUsuario(model);


                return RedirectToAction("Index", "Login");

            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("Index", "Login");

        }


    }
}
