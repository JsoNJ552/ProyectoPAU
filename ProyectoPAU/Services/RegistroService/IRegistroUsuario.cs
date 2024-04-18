using ProyectoPAU.Models;

namespace ProyectoPAU.Services.Registro
{
    public interface IRegistroUsuario
    {


        public bool IsRegistrado( string email);

        Task RegistrarUsuario(Usuario usuario);


        

    }
}
