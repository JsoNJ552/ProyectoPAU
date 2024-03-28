using ProyectoPAU.Models;

namespace ProyectoPAU.Services.UsuariosService
{
    public class UsuariosService : IUsuariosServicecs
    {


        public Task EditarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task EliminarUsuario(int IdUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> obtenerUsuario()
        {
            throw new NotImplementedException();
        }

        public Usuario obtenerUsuarioPorId(int IdUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> obtenerUsuariosFiltro(Func<Usuario, bool> filtro = null)
        {
            throw new NotImplementedException();
        }

        public Task RegistrarUsuario(Usuario usuario, IFormFile photoFile)
        {
            throw new NotImplementedException();
        }
    }
}
