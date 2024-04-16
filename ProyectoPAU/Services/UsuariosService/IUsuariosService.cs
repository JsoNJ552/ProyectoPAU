using ProyectoPAU.Models;

namespace ProyectoPAU.Services.UsuariosService
{
    public interface IUsuariosService
    {


        public Task RegistrarUsuario(Usuario usuario, IFormFile photoFile);

        public Task<List<Usuario>> obtenerUsuarios();

        public Task<List<Rol>> obtenerRoles();
   
        public  Task<Usuario> obtenerUsuarioPorId(int IdUsuario);

        public Task<Usuario> obtenerUsuarioPorEmail(Usuario email);

        public Task<IEnumerable<Usuario>> obtenerUsuariosFiltro(Func<Usuario, bool> filtro = null);

        public Task EditarUsuario(Usuario usuario);

        public Task EliminarUsuario(string email);



    }
}
