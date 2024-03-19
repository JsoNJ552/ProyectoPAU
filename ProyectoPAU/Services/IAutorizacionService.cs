using ProyectoPAU.Models.Custom;

namespace ProyectoPAU.Services
{
    public interface IAutorizacionService
    {

        Task<AutorizacionResponse>DevolverToken(AutorizacionRequest autorizacion);
        Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest,int IdUsuario);

    }
}
