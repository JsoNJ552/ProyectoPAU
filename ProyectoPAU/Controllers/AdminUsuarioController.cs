using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.Registro;
using ProyectoPAU.Services.UsuariosService;
using ProyectoPAU.Services.VentasService;

namespace ProyectoPAU.Controllers
{
   
    [Authorize(Roles = "Administrador")]
    public class AdminUsuarioController : Controller
    {
        private readonly IVentasServicecs _ventasService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TiendauContext _tiendauContext;
        private readonly IUsuariosService _usuariosService;
        private readonly IRegistroUsuario _registroService;
        


        public AdminUsuarioController(IVentasServicecs ventasServicecs,IRegistroUsuario registroUsuario, IHttpContextAccessor httpContextAccessor,
            IUsuariosService usuarioService, TiendauContext tiendauContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _ventasService = ventasServicecs;
            _tiendauContext = tiendauContext;
            _usuariosService = usuarioService;
            _registroService = registroUsuario;

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
  
        public async Task<IActionResult> VistaUsuarioRol(string Name)
        {

            try
            {
                List<Usuario> Usuarios = await _usuariosService.ObtenerUsuariosPorRol((string)Name);

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


     
        public async Task<IActionResult>RegistrarUsuario()
        {
            try
            {
                var roles = await _usuariosService.obtenerRoles();
                

                ViewData["Roles"] = roles;


              


                return View();
            }
            catch (Exception ex)
            {
                // Aquí deberías manejar el error apropiadamente en lugar de simplemente ignorarlo.
                Console.WriteLine("Error al editar el producto: " + ex.Message);
                return RedirectToAction("Index", "Error"); // Redirige a una página de error.
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarrUsuario([FromForm] Usuario usuario)
        {
            try
            {

                Console.WriteLine("NOMBRE "+" "+ usuario.Nombre );
                Console.WriteLine("APELLIDO " + " " + usuario.Apellido );
                Console.WriteLine("Email " + " " + usuario.Email );
                Console.WriteLine("ROL " + " " + usuario.RolId );
                Console.WriteLine("actvo " + " " + usuario.Activo );

                var usuarioExiste = await _usuariosService.obtenerUsuarioPorEmail(usuario);


                if(usuarioExiste != null)
                {
                    return Ok("Email ya existente");
                }

                await _registroService.RegistrarUsuario(usuario);





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
