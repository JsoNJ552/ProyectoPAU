using ProyectoPAU.Models;

namespace ProyectoPAU.Services
{
    public interface IRegistroUsuario
    {


        public bool IsRegistrado(string username, string email);

          Task RegistrarUsuario(Usuario usuario);

    }
}
