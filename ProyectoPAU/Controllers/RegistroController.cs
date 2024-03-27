using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.Registro;

namespace ProyectoPAU.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendauContext _context;
        private readonly IRegistroUsuario _registro;
 
        public RegistroController (TiendauContext context, IRegistroUsuario registro)
        {
        _context = context;
        _registro = registro;
         
            

        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public  async Task <IActionResult> Registrar(Usuario model)
        {

            if (_registro.IsRegistrado(model.Nombre, model.Email))
            {
                return View("Index");
            }

            model.RolId = 2;
            _registro.RegistrarUsuario(model);


            return RedirectToAction("Index", "Login");

        }
    }
}
