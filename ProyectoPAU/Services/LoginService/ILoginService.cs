using ProyectoPAU.Models;
using ProyectoPAU.Models.DTO;

namespace ProyectoPAU.Services.LoginService
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUserAsync(LoginUser user);
        Task<Usuario> GetUserAsync(string email);
    }
}
