using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoPAU.Models;

namespace ProyectoPAU.Services
{
    public class RegistroService : IRegistroUsuario
    {

        private readonly TiendauContext _context;


        public RegistroService(TiendauContext context)
        {
            _context = context;
            

        }


        public  bool IsRegistrado(string username, string email)
        {
            var respuesta = _context.Usuarios.FirstOrDefault(x => x.Nombre == username || x.Email == email);

            if (respuesta == null)
            {
                return false;
            }

            return true;

        }

        public async Task  RegistrarUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;

            
               
            }

        }
    }
}
