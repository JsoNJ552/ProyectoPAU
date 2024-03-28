using ProyectoPAU.Models;

namespace ProyectoPAU.Services.UsuariosService
{
    public interface IUsuariosServicecs
    {


        public Task RegistrarUsuario(Usuario usuario, IFormFile photoFile);

        public Task<List<Usuario>> obtenerUsuario();
        public Usuario obtenerUsuarioPorId(int IdUsuario);

        public Task<IEnumerable<Usuario>> obtenerUsuariosFiltro(Func<Usuario, bool> filtro = null);

        public Task EditarUsuario(Usuario usuario);

        public Task EliminarUsuario(int IdUsuario);



    }
}
