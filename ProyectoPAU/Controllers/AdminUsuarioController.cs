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


        public async Task <IActionResult> Index()
        {

            try
            {
                List<Usuario> Usuarios = await _usuariosService.obtenerUsuarios(); 

                return View(Usuarios);

            }
            catch (Exception ex)
            {


            }


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DesactivarUsuario(string email)
        {


            try
            {
                Console.WriteLine("EMAIL A DESACTIVAR"+email);
                await _usuariosService.EliminarUsuario(email);
                return RedirectToAction("Index");


            }catch (Exception ex)
            {


            }
            return View();
          
        }


        [HttpGet]
        public async Task<IActionResult> EditarUsuario(string Email)
        {


            try
            {
                Usuario usuarioBuscar = new Usuario
                {
                    Email = Email
                };

                var roles = await _usuariosService.obtenerRoles();
                ViewData["Roles"] = roles;

                Usuario usuarioEditar = await _usuariosService.obtenerUsuarioPorEmail(usuarioBuscar);
              
                Console.WriteLine("EMAIL A DESACTIVAR" + Email);
               
                return View(usuarioEditar);


            }
            catch (Exception ex)
            {


            }
            return View();

        }




        [HttpPost]
        public async Task<IActionResult> GuardarEditarUsuario([FromForm] Usuario usuario)
        {
            try
            {

             
                Usuario usuarioEditar = await _usuariosService.obtenerUsuarioPorEmail(usuario);
                usuarioEditar.Nombre = usuario.Nombre;
                usuarioEditar.Email = usuario.Email;
                usuarioEditar.Apellido = usuario.Apellido;
                usuarioEditar.RolId = usuario.RolId;
                usuarioEditar.Activo = usuario.Activo;

                await _usuariosService.EditarUsuario(usuarioEditar);

               











                return Ok("Usuario editado correctamente.");
            }
            catch (Exception ex)
            {
                // Aquí deberías manejar el error apropiadamente en lugar de simplemente ignorarlo.
                Console.WriteLine("Error al editar el producto: " + ex.Message);
                return RedirectToAction("Index", "Error"); // Redirige a una página de error.
            }
        }



    }
}
