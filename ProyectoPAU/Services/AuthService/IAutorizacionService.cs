using ProyectoPAU.Models.Custom;

namespace ProyectoPAU.Services.Auth
{
    public interface IAutorizacionService
    {

        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
        Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int IdUsuario);

    }
}
