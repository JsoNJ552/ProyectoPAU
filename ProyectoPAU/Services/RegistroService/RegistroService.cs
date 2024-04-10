using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoPAU.Models;
using BCrypt.Net;

namespace ProyectoPAU.Services.Registro
{
    public class RegistroService : IRegistroUsuario
    {

        private readonly TiendauContext _context;

        


        public RegistroService(TiendauContext context)
        {
            _context = context;


        }


        public bool IsRegistrado(string username, string email)
        {
            var respuesta = _context.Usuarios.FirstOrDefault(x =>  x.Email == email);

            if (respuesta == null)
            {
                return false;
            }

            return true;

        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            try
            {
                string HashPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                usuario.Contraseña = HashPassword;  
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
