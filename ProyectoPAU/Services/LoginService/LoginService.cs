using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;
using ProyectoPAU.Models.DTO;

namespace ProyectoPAU.Services.LoginService
{
    public class LoginService : ILoginService
    {

        private readonly TiendauContext _context;
        public LoginService(TiendauContext context)
        {
            _context = context;

        }


        public async Task<Usuario> GetUserAsync(string email)
        {
            // Obtener los datos del usuario
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> ValidateUserAsync(LoginUser loginUser)
        {
            // Verificar si el usuario y la contraseña coinciden
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginUser.email && u.Contraseña == loginUser.password);

            return user;
        }





    }



}
